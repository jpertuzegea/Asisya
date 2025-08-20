//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using static Infraestructure.Entities.Student;

namespace Infraestructure
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Students - Configuración de identidad explícita
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.StudentId);
                 
                entity.Property(e => e.StudentId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1); 

                entity.Property(e => e.IdentificationNumber).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            });
             
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teachers");
                entity.HasKey(e => e.TeacherId);
                 
                entity.Property(e => e.TeacherId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1); 

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Specialty).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.HasIndex(e => e.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            });
             
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                entity.HasKey(e => e.CourseId);
                 
                entity.Property(e => e.CourseId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1);  

                entity.Property(e => e.CourseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasColumnType("NTEXT");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

           
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grades");
                entity.HasKey(e => e.GradeId);

                 entity.Property(e => e.GradeId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1); 

                entity.Property(e => e.GradeValue).HasColumnType("decimal(4,2)").IsRequired();
                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasCheckConstraint("CK_Grade_Value", "[GradeValue] >= 0 AND [GradeValue] <= 100");
            });
        }
    }
}