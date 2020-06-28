using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using My_Website_BE.Dtos.Users;
using My_Website_BE.Helpers;
using My_Website_BE.Models;

namespace My_Website_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly ITranslator _translator;

        public UsersController(UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    IOptions<AppSettings> appSettings,
                                    ITranslator translator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _translator = translator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {

                var users = _userManager.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.FirstName,
                        u.LastName,
                        u.Email,
                        u.EmailConfirmed,
                        u.IsAdmin,
                        u.IsActive
                    })
                    .OrderBy(u => u.FirstName);
                return Ok(new { users });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public IActionResult SearchUsers([FromQuery] string searchKey)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var users = _userManager.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.FirstName,
                        u.LastName,
                        u.Email,
                        u.EmailConfirmed,
                        u.IsAdmin,
                        u.IsActive
                    })
                    .Where(u => u.FirstName.Contains(searchKey)
                               || u.LastName.Contains(searchKey)
                               || u.Email.Contains(searchKey)
                               || string.IsNullOrEmpty(searchKey))
                    .OrderBy(u => u.FirstName);

                return Ok(new { users });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }


        [AllowAnonymous]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery] string id)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userData = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    username = user.UserName,
                    email = user.Email,
                    isAdmin = user.IsAdmin,
                    isActive = user.IsActive,
                    emailConfirmed = user.EmailConfirmed
                };

                return Ok(new { user = userData });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [AllowAnonymous]
        [HttpGet("user-roles")]
        public async Task<IActionResult> GetUserRoles([FromQuery] string id)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {

                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userData = new
                {
                    isAdmin = user.IsAdmin,
                };

                return Ok(new { userRoles = userData });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody]RegUserDto userDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(userDto.FirstName))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.FIRSTNAME_REQUIRED", lang));

            if (string.IsNullOrEmpty(userDto.LastName))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.LASTNAME_REQUIRED", lang));


            if (string.IsNullOrEmpty(userDto.Email))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.EMAIL_REQUIRED", lang));

            if (string.IsNullOrEmpty(userDto.Password))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));

            if (userDto.Password != userDto.ConfirmPassword)
            {
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORDS_MATCH", lang));

                return BadRequest(new { errors = errorMessages });
            }

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });

            try
            {
                bool roleAdminExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleAdminExists)
                {
                    var roleAdmin = new IdentityRole();
                    roleAdmin.Name = "Admin";
                    await _roleManager.CreateAsync(roleAdmin);
                }

                var user = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    IsAdmin = userDto.IsAdmin.Value,
                    EmailConfirmed = userDto.EmailConfirmed.Value,
                    IsActive = true
                };

                // save 
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    if (user.IsAdmin)
                    {
                        var addToAdminResult = await _userManager.AddToRoleAsync(user, "Admin");
                        if (!addToAdminResult.Succeeded)
                        {
                            errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                            return BadRequest(new { errors = errorMessages });
                        }
                    }


                    var responseData = new
                    {
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        isAdmin = user.IsAdmin,
                        id = user.Id,
                        emailConfirmed = user.EmailConfirmed,
                        isActive = user.IsActive
                    };
                    return Ok(new { user = responseData });
                }

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
            catch
            {
                // return error message if there was an exception

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("act-deact")]
        public async Task<IActionResult> ActivateDeactivateUser([FromBody] UserActivatedDeactivateDto userActivatedDeactivateDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userActivatedDeactivateDto.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                if (userActivatedDeactivateDto.Option == "activate")
                {
                    user.IsActive = true;
                }
                else if (userActivatedDeactivateDto.Option == "deactivate")
                {
                    user.IsActive = false;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {

                    return Ok(new { userId = user.Id, isActive = user.IsActive });
                }

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });

            }
            catch
            {
                // return error message if there was an exception

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(userDto.FirstName))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.FIRSTNAME_REQUIRED", lang));

            if (string.IsNullOrEmpty(userDto.LastName))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.LASTNAME_REQUIRED", lang));


            if (string.IsNullOrEmpty(userDto.Email))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.EMAIL_REQUIRED", lang));



            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });

            try
            {
                var user = await _userManager.FindByIdAsync(userDto.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.EmailConfirmed = userDto.EmailConfirmed.Value;


                user.IsAdmin = userDto.IsAdmin.Value;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (user.IsAdmin && !(await _userManager.IsInRoleAsync(user, "Admin")))
                    {
                        var addToAdminResult = await _userManager.AddToRoleAsync(user, "Admin");
                        if (!addToAdminResult.Succeeded)
                        {
                            errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                            return BadRequest(new { errors = errorMessages });
                        }
                    }
                    else
                    {
                        var removeFromAdminResult = await _userManager.RemoveFromRoleAsync(user, "Admin");
                    }


                    var response = new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        IsAdmin = user.IsAdmin,
                        IsActive = user.IsActive
                    };

                    return Ok(new { user = response });
                }
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
            catch
            {
                // return error message if there was an exception

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }
        }


        [Authorize(Roles = "Admin, Author, User")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto userDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(userDto.FirstName))
                errorMessages.Add("First name is required");

            if (string.IsNullOrEmpty(userDto.LastName))
                errorMessages.Add("Last name is required");


            if (string.IsNullOrEmpty(userDto.Email))
                errorMessages.Add("Email is required");

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });

            try
            {
                var user = await _userManager.FindByIdAsync(userDto.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {

                    var response = new
                    {
                        id = user.Id,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        emailConfirmed = user.EmailConfirmed,
                        isAdmin = user.IsAdmin,
                        isActive = user.IsActive
                    };

                    return Ok(new { user = response });
                }

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new { userId = user.Id });
                }

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
            catch
            {
                // return error message if there was an exception

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin, Author, User")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(changePasswordDto.CurrentPassword))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));

            if (string.IsNullOrEmpty(changePasswordDto.NewPassword))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));

            if (string.IsNullOrEmpty(changePasswordDto.ConfirmPassword))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORDS_MATCH", lang));

                return BadRequest(new { errors = errorMessages });
            }

            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);

            if (!await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword))
            {

                errorMessages.Add(_translator.GetTranslation("ACCOUNT.CURRENT_PASSWORD_INCORRECT", lang));

                return BadRequest(new { errors = errorMessages });
            }

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new { result = true });
                }

                errorMessages.Add(_translator.GetTranslation("ERROR", lang));

                return BadRequest(new { errors = errorMessages });
            }

            errorMessages.Add(_translator.GetTranslation("ERROR", lang));
            return BadRequest(new { errors = errorMessages });
        }

    }
}