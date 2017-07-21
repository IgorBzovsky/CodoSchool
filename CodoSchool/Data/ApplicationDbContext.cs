﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodoSchool.Models;

namespace CodoSchool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Customizing composite primary key and foreign keys
            builder.Entity<StudentProgress>()
                .HasKey(t => new { t.ApplicationUserId, t.SectionId });

            builder.Entity<StudentProgress>()
                .HasOne(s => s.ApplicationUser)
                .WithMany(a => a.StudentProgress)
                .HasForeignKey(s => s.ApplicationUserId)
                .HasPrincipalKey(a => a.Id);

            builder.Entity<StudentProgress>()
                .HasOne(s => s.Section)
                .WithMany(sec => sec.StudentProgress)
                .HasForeignKey(s => s.SectionId)
                .HasPrincipalKey(sec => sec.Id);

            builder.Entity<Section>()
                .Property(x => x.Name)
                .IsRequired();

            builder.Entity<SectionType>()
                .Property(x => x.Name)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .Property(x => x.FirstName)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .Property(x => x.LastName)
                .IsRequired();

            builder.Entity<Question>()
                .Property(x => x.QuestionText)
                .IsRequired();

            builder.Entity<Answer>()
                .Property(x => x.AnswerText)
                .IsRequired();
        }
    }
}
