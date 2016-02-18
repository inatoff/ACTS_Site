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

        public List<string> Disciplines { get; set; }

        public List<string> ScienceInterests { get; set; }

        public List<string> Projects { get; set; }

        public List<string> Publications { get; set; }

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
            Disciplines = teacher.Disciplines.ToList();
            ScienceInterests = teacher.ScienceInterests.ToList();
            Projects = teacher.Projects.ToList();
            Publications.ToList();
        }
        private PersonalPageViewModel() { }
    }
}
