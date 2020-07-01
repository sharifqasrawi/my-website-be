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
    public class EducationsController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly ITranslator _translator;

        public EducationsController(IEducationRepository educationRepository,
                                    IDocumentRepository documentRepository,
                                    IUploadedFileRepository uploadedFileRepository,
                                    ITranslator translator)
        {
            _educationRepository = educationRepository;
            _documentRepository = documentRepository;
            _uploadedFileRepository = uploadedFileRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetEducation()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var educations = _educationRepository.GetEducations();

                return Ok(new { educations });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateEducation([FromBody] Education education)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {

                var newEdu = new Education
                {
                    Title_EN = education.Title_EN,
                    Title_FR = !string.IsNullOrEmpty(education.Title_FR) ? education.Title_FR : education.Title_EN,
                    Specialization_EN = education.Specialization_EN,
                    Specialization_FR = !string.IsNullOrEmpty(education.Specialization_FR) ? education.Specialization_FR : education.Specialization_EN,
                    Establishment_EN = education.Establishment_EN,
                    Establishment_FR = !string.IsNullOrEmpty(education.Establishment_FR) ? education.Establishment_FR : education.Establishment_EN,
                    Mention_EN = education.Mention_EN,
                    Mention_FR = !string.IsNullOrEmpty(education.Mention_FR) ? education.Mention_FR : education.Mention_EN,
                    Country_EN = education.Country_EN,
                    Country_FR = !string.IsNullOrEmpty(education.Country_FR) ? education.Country_FR : education.Country_EN,
                    City_EN = education.City_EN,
                    City_FR = !string.IsNullOrEmpty(education.City_FR) ? education.City_FR : education.City_EN,
                    YearsCount = education.YearsCount,
                    Note = education.Note,
                    StartDate = education.StartDate,
                    GraduateDate = education.GraduateDate
                };

                var createdEducation = _educationRepository.Create(newEdu);

                return Ok(new { createdEducation });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateEducation([FromBody] Education education)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var edu = _educationRepository.FindById(education.Id);
                if (edu == null)
                    return NotFound();


                edu.Title_EN = education.Title_EN;
                edu.Title_FR = !string.IsNullOrEmpty(education.Title_FR) ? education.Title_FR : education.Title_EN;
                edu.Specialization_EN = education.Specialization_EN;
                edu.Specialization_FR = !string.IsNullOrEmpty(education.Specialization_FR) ? education.Specialization_FR : education.Specialization_EN;
                edu.Establishment_EN = education.Establishment_EN;
                edu.Establishment_FR = !string.IsNullOrEmpty(education.Establishment_FR) ? education.Establishment_FR : education.Establishment_EN;
                edu.Mention_EN = education.Mention_EN;
                edu.Mention_FR = !string.IsNullOrEmpty(education.Mention_FR) ? education.Mention_FR : education.Mention_EN;
                edu.Country_EN = education.Country_EN;
                edu.Country_FR = !string.IsNullOrEmpty(education.Country_FR) ? education.Country_FR : education.Country_EN;
                edu.City_EN = education.City_EN;
                edu.City_FR = !string.IsNullOrEmpty(education.City_FR) ? education.City_FR : education.City_EN;
                edu.YearsCount = education.YearsCount;
                edu.Note = education.Note;
                edu.StartDate = education.StartDate;
                edu.GraduateDate = education.GraduateDate;
                

                var updatedEducation = _educationRepository.Update(edu);

                return Ok(new { updatedEducation });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteEducation([FromQuery] int eduId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var edu = _educationRepository.FindById(eduId);

                if (edu == null)
                    return NotFound();


                var deletedEducation = _educationRepository.Delete(eduId);

                return Ok(new { deletedEducationId = deletedEducation.Id });
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
                var education = _educationRepository.FindById(document.EducationId.Value);
                if (education == null)
                    return NotFound();

                var file = _uploadedFileRepository.FindById(document.FileId.Value);

                var newDoc = new Document
                {
                    Education = education,
                    EducationId = education.Id,
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
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
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
                var eduDoc = _documentRepository.FindById(document.Id);

                if (eduDoc == null)
                    return NotFound();

                if (document.FileId != null)
                {
                    var file = _uploadedFileRepository.FindById(document.FileId.Value);
                    eduDoc.Type = file.FileType;
                    eduDoc.FileId = document.FileId;
                }

                eduDoc.Name_EN = document.Name_EN;
                eduDoc.Name_FR = !string.IsNullOrEmpty(document.Name_FR) ? document.Name_FR : document.Name_EN;
                eduDoc.Description_EN = document.Description_EN;
                eduDoc.Description_FR = !string.IsNullOrEmpty(document.Description_FR) ? document.Description_FR : document.Description_EN;
                eduDoc.Path = document.Path;
                eduDoc.IsDisplayed = document.IsDisplayed;
                eduDoc.LastUpdatedDate = DateTime.Now;


                var updatedDocument = _documentRepository.Update(eduDoc);

                return Ok(new { updatedDocument });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
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
                var eduDoc = _documentRepository.FindById(docId);

                if (eduDoc == null)
                    return NotFound();


                var deletedDocument = _documentRepository.Delete(docId);

                return Ok(new { deletedDocumentId = deletedDocument.Id, educationId = eduDoc.EducationId });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}