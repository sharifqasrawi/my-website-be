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
    public class CVFilesController : ControllerBase
    {
        private readonly ICVFileRepository _cVFileRepository;
        private readonly ITranslator _translator;

        public CVFilesController(ICVFileRepository cVFileRepository,
                                 ITranslator translator)
        {
            _cVFileRepository = cVFileRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCVFiles()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var cvFiles = _cVFileRepository.GetCVFiles();

                return Ok(new { cvFiles });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCVFile([FromBody] CVFile cVFile)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var newCvFile = new CVFile
                {
                    FileName = cVFile.FileName,
                    Language = cVFile.Language,
                    FilePath = cVFile.FilePath,
                    LastUpdateDate = DateTime.Now,
                };

                var createdCVFile = _cVFileRepository.Create(newCvFile);

                return Ok(new { createdCVFile });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateCVFile([FromBody] CVFile cVFile)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var cv = _cVFileRepository.FindById(cVFile.Id);
                if (cv == null)
                    return NotFound();


                cv.FileName = cVFile.FileName;
                cv.Language = cVFile.Language;
                cv.FilePath = cVFile.FilePath;
                cv.LastUpdateDate = DateTime.Now;
                

                var updatedCVFile = _cVFileRepository.Update(cv);

                return Ok(new { updatedCVFile });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteCVFile([FromQuery] int cvId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var cv = _cVFileRepository.FindById(cvId);
                if (cv == null)
                    return NotFound();

                var deletedCVFile = _cVFileRepository.Delete(cvId);

                return Ok(new { deletedCVFileId = deletedCVFile.Id });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}