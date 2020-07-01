using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {

        }

        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<CVFile> CVFiles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TrainingCourse> TrainingCourses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
