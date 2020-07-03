using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Helpers
{
    public static class ResponseGenerator
    {
        public static object GenerateProjectResponse(Project project)
        {
            // Tags
            var tags = new List<Tag>();

            if (project.ProjectTags != null)
            {
                foreach (var projectTag in project.ProjectTags)
                {
                    tags.Add(new Tag() { Id = projectTag.TagId, Name = projectTag.Tag.Name });
                }
            }

            var response = new
            {
                project.Id,
                project.Name_EN,
                project.Name_FR,
                project.Description_EN,
                project.Description_FR,
                project.Type,
                project.Size,
                project.GitHubUrl,
                project.ImagePath,
                project.VideoDemoUrl,
                project.VideoDemoUrlExt,
                project.LiveDemoUrl,
                project.IsDisplayed,
                tags
            };

            return response;
        }
    }
}
