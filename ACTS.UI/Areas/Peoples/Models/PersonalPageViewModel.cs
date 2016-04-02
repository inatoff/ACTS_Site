using ACTS.Core.Entities;
using ACTS.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Peoples.Models
{
    public class PersonalPageViewModel
    {
        public string FullName { get; }
        public string Degree { get; }
        public string Position { get; }
        public string Email { get; }
        public string Intellect { get; }
        public string Vk { get; }
        public string Facebook { get; }
        public string Twitter { get; }

        public string Greetings { get; }
        public Rank Rank { get; }
        public Blog Blog { get; }

        public IEnumerable<string> Disciplines { get; set; }

        public IEnumerable<string> ScienceInterests { get; set; }

        public IEnumerable<string> Projects { get; set; }

        public IEnumerable<string> Publications { get; set; }

        [Display(Name = nameof(DisplayRes.PhotoName), ResourceType = typeof(DisplayRes))]
        public byte[] Photo { get; set; }

        public string PhotoMimeType { get; set; }


        public PersonalPageViewModel(Teacher teacher)
        {
            FullName = teacher.FullName;
            Degree = teacher.Degree;
            Greetings = teacher.Greetings;
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
            Photo = teacher.Photo;
            PhotoMimeType = teacher.PhotoMimeType;
            Position = teacher.Position;
            Rank = teacher.Rank;
        }
        private PersonalPageViewModel() { }
    }
}
