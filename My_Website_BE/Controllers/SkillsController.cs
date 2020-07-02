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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillCategoryRepository _skillCategoryRepository;
        private readonly ITranslator _translator;

        public SkillsController(ISkillRepository skillRepository,
                                ISkillCategoryRepository  skillCategoryRepository,
                                ITranslator translator)
        {
            _skillRepository = skillRepository;
            _skillCategoryRepository = skillCategoryRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet("categories")]
        public IActionResult GetSkillCategories()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var skillCategories = _skillCategoryRepository.GetSkillCategories();

                return Ok(new { skillCategories });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-category")]
        public IActionResult CreateSkillCategory([FromBody] SkillCategory skillCategory)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {

                var newCat = new SkillCategory
                {
                    Name_EN = skillCategory.Name_EN,
                    Name_FR = !string.IsNullOrEmpty(skillCategory.Name_FR) ? skillCategory.Name_FR : skillCategory.Name_EN
                };

                var createdSkillCategory = _skillCategoryRepository.Create(newCat);

                return Ok(new { createdSkillCategory });
            }
            catch (Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("update-category")]
        public IActionResult UpdateSkillCategory([FromBody] SkillCategory skillCategory)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var category = _skillCategoryRepository.FindById(skillCategory.Id);
                if (category == null)
                    return NotFound();

                category.Name_EN = skillCategory.Name_EN;
                category.Name_FR = !string.IsNullOrEmpty(skillCategory.Name_FR) ? skillCategory.Name_FR : skillCategory.Name_EN;
               

                var updatedSkillCategory = _skillCategoryRepository.Update(category);

                return Ok(new { updatedSkillCategory });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-category")]
        public IActionResult DeleteSkillCategory([FromQuery] int catId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var category = _skillCategoryRepository.FindById(catId);
                if (category == null)
                    return NotFound();

                var deletedSkillCategory = _skillCategoryRepository.Delete(catId);

                return Ok(new { deletedSkillCategoryId = deletedSkillCategory.Id });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }


        //////////////

        [AllowAnonymous]
        [HttpGet("skills")]
        public IActionResult GetSkills()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var skills = _skillRepository.GetSkills();

                return Ok(new { skills });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateSkill([FromBody] Skill skill)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var category = _skillCategoryRepository.FindById(skill.CategoryId);
                if (category == null)
                    return NotFound();

                var newSkill = new Skill
                {
                    Category = category,
                    CategoryId = category.Id,
                    Name_EN = skill.Name_EN,
                    Name_FR = !string.IsNullOrEmpty(skill.Name_FR) ? skill.Name_FR : skill.Name_EN,
                    Level = skill.Level
                };

                var createdSkill = _skillRepository.Create(newSkill);

                return Ok(new { createdSkill });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateSkill([FromBody] Skill skill)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var skl = _skillRepository.FindById(skill.Id);
                if (skl == null)
                    return NotFound();


                skl.Name_EN = skill.Name_EN;
                skl.Name_FR = !string.IsNullOrEmpty(skill.Name_FR) ? skill.Name_FR : skill.Name_EN;
                skl.Level = skill.Level;
                

                var updatedSkill = _skillRepository.Update(skl);

                return Ok(new { updatedSkill });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteSkill([FromQuery] int skillId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();

            try
            {
                var skill = _skillRepository.FindById(skillId);
                if (skill == null)
                    return NotFound();

                var deletedSkill = _skillRepository.Delete(skillId);

                return Ok(new { deletedSkillId = deletedSkill.Id, categoryId = skill.CategoryId });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}