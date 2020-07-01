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
    public class CoursesController : ControllerBase
    {
        private readonly ITrainingCourseRepository _trainingCourseRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly ITranslator _translator;

        public CoursesController(ITrainingCourseRepository trainingCourseRepository,
                                IDocumentRepository documentRepository,
                                IUploadedFileRepository uploadedFileRepository,
                                ITranslator translator)
        {
            _trainingCourseRepository = trainingCourseRepository;
            _documentRepository = documentRepository;
            _uploadedFileRepository = uploadedFileRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCourses()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var courses = _trainingCourseRepository.GetTrainingCourses();

                return Ok(new { courses });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateCourse([FromBody] TrainingCourse trainingCourse)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {

                var newCourse = new TrainingCourse
                {
                    Name = trainingCourse.Name,
                    CourseUrl = trainingCourse.CourseUrl,
                    Type = trainingCourse.Type,
                    Establishment = trainingCourse.Establishment,
                    Duration = trainingCourse.Duration,
                    DateTime = trainingCourse.DateTime,
                    Country_EN = trainingCourse.Country_EN,
                    Country_FR = !string.IsNullOrEmpty(trainingCourse.Country_FR) ? trainingCourse.Country_FR : trainingCourse.Country_EN,
                    City_EN = trainingCourse.City_EN,
                    City_FR = !string.IsNullOrEmpty(trainingCourse.City_FR) ? trainingCourse.City_FR : trainingCourse.City_EN,

                };

                var createdCourse = _trainingCourseRepository.Create(newCourse);

                return Ok(new { createdCourse });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateCourse([FromBody] TrainingCourse trainingCourse)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var course = _trainingCourseRepository.FindById(trainingCourse.Id);
                if (course == null)
                    return NotFound();

                course.Name = trainingCourse.Name;
                course.CourseUrl = trainingCourse.CourseUrl;
                course.Type = trainingCourse.Type;
                course.Establishment = trainingCourse.Establishment;
                course.Duration = trainingCourse.Duration;
                course.DateTime = trainingCourse.DateTime;
                course.Country_EN = trainingCourse.Country_EN;
                course.Country_FR = !string.IsNullOrEmpty(trainingCourse.Country_FR) ? trainingCourse.Country_FR : trainingCourse.Country_EN;
                course.City_EN = trainingCourse.City_EN;
                course.City_FR = !string.IsNullOrEmpty(trainingCourse.City_FR) ? trainingCourse.City_FR : trainingCourse.City_EN;


                var updatedCourse = _trainingCourseRepository.Update(course);

                return Ok(new { updatedCourse });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteCourse([FromQuery] int courseId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var course = _trainingCourseRepository.FindById(courseId);
                if (course == null)
                    return NotFound();

                var deletedCourse = _trainingCourseRepository.Delete(courseId);

                return Ok(new { deletedCourseId = deletedCourse.Id });
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
                var course = _trainingCourseRepository.FindById(document.CourseId.Value);
                if (course == null)
                    return NotFound();

                var file = _uploadedFileRepository.FindById(document.FileId.Value);

                var newDoc = new Document
                {
                    Course = course,
                    CourseId = course.Id,
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
                var courseDoc = _documentRepository.FindById(document.Id);
                if (courseDoc == null)
                    return NotFound();

                if (document.FileId != null)
                {
                    var file = _uploadedFileRepository.FindById(document.FileId.Value);
                    courseDoc.Type = file.FileType;
                    courseDoc.FileId = document.FileId;
                }

                courseDoc.Name_EN = document.Name_EN;
                courseDoc.Name_FR = !string.IsNullOrEmpty(document.Name_FR) ? document.Name_FR : document.Name_EN;
                courseDoc.Description_EN = document.Description_EN;
                courseDoc.Description_FR = !string.IsNullOrEmpty(document.Description_FR) ? document.Description_FR : document.Description_EN;
                courseDoc.Path = document.Path;
                courseDoc.IsDisplayed = document.IsDisplayed;
                courseDoc.LastUpdatedDate = DateTime.Now;

                var updatedDocument = _documentRepository.Update(courseDoc);

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
                var courseDoc = _documentRepository.FindById(docId);
                if (courseDoc == null)
                    return NotFound();

                var deletedCourseDocument = _documentRepository.Delete(docId);

                return Ok(new { deletedDocumentId = deletedCourseDocument.Id, courseId = courseDoc.CourseId });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

    }
}