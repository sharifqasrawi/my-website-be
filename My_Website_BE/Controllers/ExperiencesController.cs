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
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly ITranslator _translator;

        public ExperiencesController(IExperienceRepository experienceRepository,
                                IDocumentRepository documentRepository,
                                IUploadedFileRepository uploadedFileRepository,
                                ITranslator translator)
        {
            _experienceRepository = experienceRepository;
            _documentRepository = documentRepository;
            _uploadedFileRepository = uploadedFileRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetExperiences()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var experiences = _experienceRepository.GetExperiences();

                return Ok(new { experiences });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateExperience([FromBody] Experience experience)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {

                var newExp = new Experience
                {
                    Title_EN = experience.Title_EN,
                    Title_FR = !string.IsNullOrEmpty(experience.Title_FR) ? experience.Title_FR : experience.Title_EN,
                    Company = experience.Company,
                    Country_EN = experience.Country_EN,
                    Country_FR = !string.IsNullOrEmpty(experience.Country_FR) ? experience.Country_FR : experience.Country_EN,
                    City_EN = experience.City_EN,
                    City_FR = !string.IsNullOrEmpty(experience.City_FR) ? experience.City_FR : experience.City_EN,
                    Accomplishments_EN = experience.Accomplishments_EN,
                    Accomplishments_FR = !string.IsNullOrEmpty(experience.Accomplishments_FR) ? experience.Accomplishments_FR : experience.Accomplishments_EN,
                    Responisbilites_EN = experience.Responisbilites_EN,
                    Responisbilites_FR = !string.IsNullOrEmpty(experience.Responisbilites_FR) ? experience.Responisbilites_FR : experience.Responisbilites_EN,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    IsCurrentlyWorking = experience.IsCurrentlyWorking
                };

                var createdExperience = _experienceRepository.Create(newExp);

                return Ok(new { createdExperience });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateExperience([FromBody] Experience experience)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var exp = _experienceRepository.FindById(experience.Id);
                if (exp == null)
                    return NotFound();


                exp.Title_EN = experience.Title_EN;
                exp.Title_FR = !string.IsNullOrEmpty(experience.Title_FR) ? experience.Title_FR : experience.Title_EN;
                exp.Company = experience.Company;
                exp.Country_EN = experience.Country_EN;
                exp.Country_FR = !string.IsNullOrEmpty(experience.Country_FR) ? experience.Country_FR : experience.Country_EN;
                exp.City_EN = experience.City_EN;
                exp.City_FR = !string.IsNullOrEmpty(experience.City_FR) ? experience.City_FR : experience.City_EN;
                exp.Accomplishments_EN = experience.Accomplishments_EN;
                exp.Accomplishments_FR = !string.IsNullOrEmpty(experience.Accomplishments_FR) ? experience.Accomplishments_FR : experience.Accomplishments_EN;
                exp.Responisbilites_EN = experience.Responisbilites_EN;
                exp.Responisbilites_FR = !string.IsNullOrEmpty(experience.Responisbilites_FR) ? experience.Responisbilites_FR : experience.Responisbilites_EN;
                exp.StartDate = experience.StartDate;
                exp.EndDate = experience.EndDate;
                exp.IsCurrentlyWorking = experience.IsCurrentlyWorking;

                var updatedExperience = _experienceRepository.Update(exp);

                return Ok(new { updatedExperience });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteExperience([FromQuery] int expId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var exp = _experienceRepository.FindById(expId);
                if (exp == null)
                    return NotFound();

                var deletedExperience = _experienceRepository.Delete(expId);

                return Ok(new { deletedExperienceId = deletedExperience.Id });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-document")]
        public IActionResult CreateDocument([FromBody] Document document)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var experience = _experienceRepository.FindById(document.ExperienceId.Value);
                if (experience == null)
                    return NotFound();

                var file = _uploadedFileRepository.FindById(document.FileId.Value);

                var newDoc = new Document
                {
                    Experience = experience,
                    ExperienceId = experience.Id,
                    Name_EN = document.Name_EN,
                    Name_FR = !string.IsNullOrEmpty(document.Name_FR) ? document.Name_FR : document.Name_EN,
                    Description_EN = document.Description_EN,
                    Description_FR = !string.IsNullOrEmpty(document.Description_FR) ? document.Description_FR : document.Description_EN,
                    Path = document.Path,
                    IsDisplayed = document.IsDisplayed,
                    LastUpdatedDate = DateTime.Now,
                    Type = file.FileType,
                    FileId = document.FileId
                };

                var createdDocument = _documentRepository.Create(newDoc);

                return Ok(new { createdDocument });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-document")]
        public IActionResult UpdateDocument([FromBody] Document document)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var expDoc = _documentRepository.FindById(document.Id);

                if (expDoc == null)
                    return NotFound();

                if (document.FileId != null)
                {
                    var file = _uploadedFileRepository.FindById(document.FileId.Value);
                    expDoc.Type = file.FileType;
                    expDoc.FileId = document.FileId;
                }
                expDoc.Name_EN = document.Name_EN;
                expDoc.Name_FR = !string.IsNullOrEmpty(document.Name_FR) ? document.Name_FR : document.Name_EN;
                expDoc.Description_EN = document.Description_EN;
                expDoc.Description_FR = !string.IsNullOrEmpty(document.Description_FR) ? document.Description_FR : document.Description_EN;
                expDoc.Path = document.Path;
                expDoc.IsDisplayed = document.IsDisplayed;
                expDoc.LastUpdatedDate = DateTime.Now;
                


                var updatedDocument = _documentRepository.Update(expDoc);

                return Ok(new { updatedDocument });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-document")]
        public IActionResult DeleteDocument([FromQuery] int docId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var expDoc = _documentRepository.FindById(docId);

                if (expDoc == null)
                    return NotFound();


                var deletedDocument = _documentRepository.Delete(docId);

                return Ok(new { deletedDocumentId = deletedDocument.Id, experienceId = expDoc.ExperienceId });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}