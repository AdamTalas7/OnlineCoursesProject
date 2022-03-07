using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWeb.Models
{
    public class StudentSearch
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }
    }
}
