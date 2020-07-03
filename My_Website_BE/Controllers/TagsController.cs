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
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITranslator _translator;

        public TagsController(ITagRepository tagRepository, ITranslator translator)
        {
            _tagRepository = tagRepository;
            _translator = translator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetTags()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var tags = _tagRepository.GetTags();

                return Ok(new { tags });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("tag")]
        public IActionResult GetTagById([FromQuery] int id)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var tag = _tagRepository.FindById(id);

                return Ok(new { tag });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] Tag tag)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var newTag = new Tag()
                {
                    Name = tag.Name
                };

                var createdTag = _tagRepository.Create(newTag);

                if (createdTag == null)
                {
                    errorMessages.Add("Error creating tag");
                    return BadRequest(new { errors = errorMessages });
                }

                return Ok(new { createdTag });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult Update([FromBody] Tag tag)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var t = _tagRepository.FindById(tag.Id);
                if (t == null)
                    return NotFound();

                t.Name = tag.Name;

                var updatedTag = _tagRepository.Update(t);

                return Ok(new { updatedTag });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var tag = _tagRepository.FindById(id);

                if (tag == null)
                    return NotFound();

                var deletedTag = _tagRepository.Delete(id);

                return Ok(new { deletedTagId = deletedTag.Id });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}