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
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly ITranslator _translator;

        public ContactInfoController(IContactInfoRepository contactInfoRepository,
                                      ITranslator translator)
        {
            _contactInfoRepository = contactInfoRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetContactInfo()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var contactInfo = _contactInfoRepository.GetContact();
                if (contactInfo == null)
                {
                    var newCI = new ContactInfo
                    {
                        Emails = "",
                        Phone = "",
                        LinkedInUrl = "",
                        GitHubUrl = "",
                        FacebookUrl = "",
                        Country_EN = "",
                        Country_FR = "",
                        City_EN = "",
                        City_FR = "",
                        ZipCode = "",
                        Street = "",
                        StreetNumber = ""
                    };
                    contactInfo = _contactInfoRepository.Create(newCI);
                }

                return Ok(new { contactInfo });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [HttpPut]
        public IActionResult UpdateContactInfo([FromBody] ContactInfo contactInfo)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var ci = _contactInfoRepository.GetContact();

                ci.Emails = contactInfo.Emails;
                ci.Phone = contactInfo.Phone;
                ci.LinkedInUrl = contactInfo.LinkedInUrl;
                ci.GitHubUrl = contactInfo.GitHubUrl;
                ci.FacebookUrl = contactInfo.FacebookUrl;
                ci.Country_EN = contactInfo.Country_EN;
                ci.Country_FR = string.IsNullOrEmpty(contactInfo.Country_FR) ? contactInfo.Country_EN : contactInfo.Country_FR;
                ci.City_EN = contactInfo.City_EN;
                ci.City_FR = string.IsNullOrEmpty(contactInfo.City_FR) ? contactInfo.City_EN : contactInfo.City_FR;
                ci.Street = contactInfo.Street;
                ci.StreetNumber = contactInfo.StreetNumber;
                ci.ZipCode = contactInfo.ZipCode;
               

                var updatedContactInfo = _contactInfoRepository.Update(ci);

                return Ok(new { updatedContactInfo });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}