using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagment.Models;

namespace StudentManagment.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<RegisterStudent> RegisterStudents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
