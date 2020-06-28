using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using My_Website_BE.Dtos.Users;
using My_Website_BE.Emailing;
using My_Website_BE.Helpers;
using My_Website_BE.Models;

namespace My_Website_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly ITranslator _translator;

        public AccountController(UserManager<ApplicationUser> userManager,
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginDto loginDto)
        {
            var errorMessages = new List<string>();
            var lang = Request.Headers["language"].ToString();

            if (string.IsNullOrEmpty(loginDto.Email))
            {
                errorMessages.Add(_translator.GetTranslation("VALIDATION.EMAIL_REQUIRED", lang));
            }

            if (string.IsNullOrEmpty(loginDto.Password))
            {
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));
            }

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });


            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && !user.IsActive)
            {
                errorMessages.Add(_translator.GetTranslation("ACCOUNT.DEACTIVATED", lang));

                return BadRequest(new { errors = errorMessages });
            }


            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                errorMessages.Add(_translator.GetTranslation("ACCOUNT.INVALID_USERNAME_PASSWORD", lang));


                return BadRequest(new { errors = errorMessages });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                id = user.Id,
                username = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                isAdmin = user.IsAdmin,
                token = tokenString,
                expiresIn = tokenDescriptor.Expires,
                isActive = user.IsActive
            });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginDto userDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            if (string.IsNullOrEmpty(userDto.Email))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.EMAIL_REQUIRED", lang));

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });


            var user = await _userManager.FindByEmailAsync(userDto.Email);

            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                try
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPwdLink = "https://qasrawi.fr/security/reset-password?email=" + user.Email + "&token=" + resetToken.ToString();
                    string To = user.Email;
                    string Subject = _translator.GetTranslation("ACCOUNT.RESET_PASSWORD_EMAIL_SUBJECT", lang);
                    string Body = _translator.GetTranslation("ACCOUNT.RESET_PASSWORD_EMAIL_MESSAGE", lang) + " : " + $"<br><a href=\"{resetPwdLink}\"> {resetPwdLink}</a>";
                    Email email = new Email(To, Subject, Body);
                    email.Send();

                    return Ok(true);

                }
                catch
                {
                    errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                    return BadRequest(new { errors = errorMessages });
                }

            }

            errorMessages.Add(_translator.GetTranslation("ACCOUNT.CANNOT_RESET_PASSWORD", lang));
            return BadRequest(new { errors = errorMessages });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(resetPasswordDto.Password))
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORD_REQUIRED", lang));

            if (errorMessages.Count > 0)
                return BadRequest(new { errors = errorMessages });


            if (resetPasswordDto.Password != resetPasswordDto.ConfirmPassword)
            {
                errorMessages.Add(_translator.GetTranslation("VALIDATION.PASSWORDS_MATCH", lang));

                return BadRequest(new { errors = errorMessages });
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                if (result.Succeeded)
                {
                    return Ok(true);
                }


                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }

            errorMessages.Add(_translator.GetTranslation("ERROR", lang));
            return BadRequest(new { errors = errorMessages });
        }

    }
}