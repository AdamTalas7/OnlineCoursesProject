using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWeb.Models
{
    public class TeacherSearch
    {
        public string Course { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
        public List<Teacher> Teachers { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Languages { get; set; }
        public SelectList Levels { get; set; }
    }
}
