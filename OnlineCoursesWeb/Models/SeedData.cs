using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesWeb.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWeb.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext database = new ApplicationDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!database.Teacher.Any())
            {
                var lines = File.ReadAllLines(@"C:\Users\Forszt\Downloads\OnlineCoursesProject-master\teachers.csv").Skip(1);
                foreach (var line in lines)
                {
                    database.Teacher.Add(new Teacher(line));
                }
                database.SaveChanges();
            }
        }
    }
}
