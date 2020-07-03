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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITagRepository _tagRepository;
        private ITranslator _translator;

        public ProjectsController(IProjectRepository projectRepository,
                                  ITagRepository tagRepository,
                                  ITranslator translator)
        {
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _translator = translator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetProjects()
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var projectsd = _projectRepository.GetProjects();

                List<object> projects = new List<object>();
                foreach(var project in projectsd)
                {
                    projects.Add(ResponseGenerator.GenerateProjectResponse(project));
                }

                return Ok(new { projects });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateProject([FromBody] Project project)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var newProject = new Project
                {
                    Name_EN = project.Name_EN,
                    Name_FR = !string.IsNullOrEmpty(project.Name_FR) ? project.Name_FR : project.Name_EN,
                    Description_EN = project.Description_EN,
                    Description_FR = !string.IsNullOrEmpty(project.Description_FR) ? project.Description_FR : project.Description_EN,
                    Type = project.Type,
                    Size = project.Size,
                    GitHubUrl = project.GitHubUrl,
                    IsDisplayed = project.IsDisplayed,
                    LiveDemoUrl = project.LiveDemoUrl,
                    VideoDemoUrl = project.VideoDemoUrl,
                    VideoDemoUrlExt = project.VideoDemoUrlExt,
                    ImagePath = project.ImagePath
                };

                var createdProject = _projectRepository.Create(newProject);

                return Ok(new { createdProject = ResponseGenerator.GenerateProjectResponse(createdProject) });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }


        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateProject([FromBody] Project project)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var prj = _projectRepository.FindById(project.Id);
                if (prj == null) return NotFound();

                prj.Name_EN = project.Name_EN;
                prj.Name_FR = !string.IsNullOrEmpty(project.Name_FR) ? project.Name_FR : project.Name_EN;
                prj.Description_EN = project.Description_EN;
                prj.Description_FR = !string.IsNullOrEmpty(project.Description_FR) ? project.Description_FR : project.Description_EN;
                prj.Type = project.Type;
                prj.Size = project.Size;
                prj.GitHubUrl = project.GitHubUrl;
                prj.IsDisplayed = project.IsDisplayed;
                prj.LiveDemoUrl = project.LiveDemoUrl;
                prj.VideoDemoUrl = project.VideoDemoUrl;
                prj.VideoDemoUrlExt = project.VideoDemoUrlExt;
                prj.ImagePath = project.ImagePath;

                var updatedProject = _projectRepository.Update(prj);

                return Ok(new { updatedProject = ResponseGenerator.GenerateProjectResponse(updatedProject) });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }


        [AllowAnonymous]
        [HttpDelete]
        public IActionResult DeleteProject([FromQuery] int projectId)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var prj = _projectRepository.FindById(projectId);
                if (prj == null) return NotFound();

                var deletedProject = _projectRepository.Delete(projectId);

                return Ok(new { deletedProjectId = deletedProject.Id });
            }
            catch
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                return BadRequest(new { errors = errorMessages });
            }
        }

        ////// Tags //////

        [Authorize(Roles = "Admin")]
        [HttpPut("tag-project")]
        public IActionResult AddTagToproject([FromBody] ProjectTag projectTag, [FromQuery] string action)
        {
            var lang = Request.Headers["language"].ToString();
            var errorMessages = new List<string>();
            try
            {
                var project = _projectRepository.FindById(projectTag.ProjectId);
                var projectTags = project.ProjectTags;

                if (action == "add")
                {
                    var newTag = _tagRepository.FindById(projectTag.TagId);
                    var newProjectTag = new ProjectTag()
                    {
                        ProjectId = projectTag.ProjectId,
                        Project = project,
                        TagId = projectTag.TagId,
                        Tag = newTag
                    };
                    projectTags.Add(newProjectTag);
                }
                else if (action == "remove")
                {
                    var currentProjectTag = projectTags
                                 .SingleOrDefault(x => x.TagId == projectTag.TagId && x.ProjectId == projectTag.ProjectId);

                    projectTags.Remove(currentProjectTag);
                }
                project.ProjectTags = projectTags;

                var updatedproject = _projectRepository.Update(project);

                var response = ResponseGenerator.GenerateProjectResponse(updatedproject);

                return Ok(new { updatedProject = response });
            }
            catch(Exception ex)
            {
                errorMessages.Add(_translator.GetTranslation("ERROR", lang));
                errorMessages.Add(ex.Message);
                return BadRequest(new { errors = errorMessages });
            }
        }
    }
}