using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineCoursesWeb.Models;

namespace OnlineCoursesWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OnlineCoursesWeb.Models.Teacher> Teacher { get; set; }
        public DbSet<OnlineCoursesWeb.Models.Student> Student { get; set; }
    }
}
