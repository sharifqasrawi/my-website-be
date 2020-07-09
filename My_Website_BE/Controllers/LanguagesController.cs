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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUploadedFileRepository _uploadedFileRepository;
        private readonly ITranslator _translator;

        public LanguagesController(ILanguageRepository languageRepository,
                                IDocumentRepository documentRepository,
                                 IUploadedFileRepository uploadedFileRepository,
                                ITranslator translator)
        {
            _languageRepository = languageRepository;
            _documentRepository = documentRepository;
            _uploadedFileRepository = uploadedFileRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetLanguages()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var languages = _languageRepository.GetLanguages()
                                                    .Select(l => new
                                                    {
                                                        l.Id,
                                                        l.Name_EN,
                                                        l.Name_FR,
                                                        l.LevelRead,
                                                        l.LevelSpeak,
                                                        l.LevelWrite,
                                                        documents = l.Documents.Where(d => d.IsDisplayed.Value)
                                                    }).ToList();

                return Ok(new { languages });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetLanguagesAdmin()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var languages = _languageRepository.GetLanguages();

                return Ok(new { languages });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateLanguage([FromBody] Language language)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {

                var newLang = new Language
                {
                    Name_EN = language.Name_EN,
                    Name_FR = !string.IsNullOrEmpty(language.Name_FR) ? language.Name_FR : language.Name_EN,
                    LevelRead = language.LevelRead,
                    LevelWrite = language.LevelWrite,
                    LevelSpeak = language.LevelSpeak,
                };

                var createdLanguage = _languageRepository.Create(newLang);

                return Ok(new { createdLanguage });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateLanguage([FromBody] Language language)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var l = _languageRepository.FindById(language.Id);
                if (l == null)
                    return NotFound();

                l.Name_EN = language.Name_EN;
                l.Name_FR = !string.IsNullOrEmpty(language.Name_FR) ? language.Name_FR : language.Name_EN;
                l.LevelRead = language.LevelRead;
                l.LevelWrite = language.LevelWrite;
                l.LevelSpeak = language.LevelSpeak;

                var updatedLanguage = _languageRepository.Update(l);

                return Ok(new { updatedLanguage });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteLanguage([FromQuery] int langId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var l = _languageRepository.FindById(langId);
                if (l == null)
                    return NotFound();

                var deletedLanguage = _languageRepository.Delete(langId);

                return Ok(new { deletedLanguageId = deletedLanguage.Id });
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
                var language = _languageRepository.FindById(document.LanguageId.Value);
                if (language == null)
                    return NotFound();

                var file = _uploadedFileRepository.FindById(document.FileId.Value);

                var newDoc = new Document
                {
                    Language = language,
                    LanguageId = language.Id,
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
                var languageDoc = _documentRepository.FindById(document.Id);
                if (languageDoc == null)
                    return NotFound();

                if (document.FileId != null)
                {
                    var file = _uploadedFileRepository.FindById(document.FileId.Value);
                    languageDoc.Type = file.FileType;
                    languageDoc.FileId = document.FileId;
                }

                languageDoc.Name_EN = document.Name_EN;
                languageDoc.Name_FR = !string.IsNullOrEmpty(document.Name_FR) ? document.Name_FR : document.Name_EN;
                languageDoc.Description_EN = document.Description_EN;
                languageDoc.Description_FR = !string.IsNullOrEmpty(document.Description_FR) ? document.Description_FR : document.Description_EN;
                languageDoc.Path = document.Path;
                languageDoc.IsDisplayed = document.IsDisplayed;
                languageDoc.LastUpdatedDate = DateTime.Now;


                var updatedDocument = _documentRepository.Update(languageDoc);

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
                var languageDoc = _documentRepository.FindById(docId);
                if (languageDoc == null)
                    return NotFound();

                var deletedDocument = _documentRepository.Delete(docId);

                return Ok(new { deletedDocumentId = deletedDocument.Id, languageId = languageDoc.LanguageId });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}