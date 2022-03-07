using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWeb.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string Course { get; set; }
        public string Level { get; set; }

        public Teacher()
        {
        }

        public Teacher(string line)
        {
            string[] array = line.Split(";");
            Name = array[0];
            Email = array[1];
            Language = array[2];
            Course = array[3];
            Level = array[4];
        }
    }
}
