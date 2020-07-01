using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Website_BE.Helpers;
using My_Website_BE.Models;
using My_Website_BE.Repositories;

namespace My_Website_BE.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PersonalInfoController : ControllerBase
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;
        private readonly ITranslator _translator;

        public PersonalInfoController(IPersonalInfoRepository personalInfoRepository,
                                      ITranslator translator)
        {
            _personalInfoRepository = personalInfoRepository;
            _translator = translator;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetPersonalInfo()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var personalInfo = _personalInfoRepository.GetPersonalInfo();
                if(personalInfo == null)
                {
                    var newPI = new PersonalInfo
                    {
                        Name = "",
                        About_EN = "",
                        About_FR = "",
                        DateOfBirth = null,
                        ImagePath = "",
                        MaritalStatus = "",
                        Title_EN = "",
                        Title_FR = "",
                        DriversLicense = null
                    };
                    personalInfo = _personalInfoRepository.Create(newPI);
                }

                return Ok(new { personalInfo });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [HttpPut]
        public IActionResult UpdatePersonalInfo([FromBody] PersonalInfo personalInfo)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var pi = _personalInfoRepository.GetPersonalInfo();

                pi.Name = personalInfo.Name;
                pi.Title_EN = personalInfo.Title_EN;
                pi.Title_FR = string.IsNullOrEmpty(personalInfo.Title_FR) ? personalInfo.Title_EN: personalInfo.Title_FR;
                pi.About_EN = personalInfo.About_EN;
                pi.About_FR = string.IsNullOrEmpty(personalInfo.About_FR) ? personalInfo.About_EN: personalInfo.About_FR;
                pi.DateOfBirth = personalInfo.DateOfBirth;
                pi.ImagePath = personalInfo.ImagePath;
                pi.MaritalStatus = personalInfo.MaritalStatus;
                pi.DriversLicense = personalInfo.DriversLicense;

                var updatedPersonalInfo = _personalInfoRepository.Update(pi);

                return Ok(new { updatedPersonalInfo });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}