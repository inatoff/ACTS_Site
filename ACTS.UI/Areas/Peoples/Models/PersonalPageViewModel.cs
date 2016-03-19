using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Peoples.Models
{
    public class PersonalPageViewModel
    {
        public string FullName { get; }
        public string Degree { get; }
        public string Email { get; }
        public string Intellect { get; }
        public string Vk { get; }
        public string Facebook { get; }
        public string Twitter { get; }
        public Blog Blog { get; }

        public IEnumerable<string> Disciplines { get; set; }

        public IEnumerable<string> ScienceInterests { get; set; }

        public IEnumerable<string> Projects { get; set; }

        public IEnumerable<string> Publications { get; set; }

        public PersonalPageViewModel(Teacher teacher)
        {
            FullName = teacher.FullName;
            Degree = teacher.Degree;
            Email = teacher.Email;
            Intellect = teacher.Intellect;
            Vk = teacher.Vk;
            Facebook = teacher.Facebook;
            Twitter = teacher.Twitter;
            Blog = teacher.Blog;
            Disciplines = teacher.Disciplines;
            ScienceInterests = teacher.ScienceInterests;
            Projects = teacher.Projects;
            Publications = teacher.Publications;
        }
        private PersonalPageViewModel() { }
    }
}
