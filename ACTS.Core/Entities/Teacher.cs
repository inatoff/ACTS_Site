using ACTS.Core.Abstract;
using ACTS.Core.Identity;
using ACTS.Localization.Resources;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.Core.Entities
{
    public enum Rank
    {
        [Display(Name = nameof(DisplayRes.Head), ResourceType = typeof(DisplayRes))]
        Head,
        [Display(Name = nameof(DisplayRes.FirstVice), ResourceType = typeof(DisplayRes))]
        FirstVice,
        [Display(Name = nameof(DisplayRes.Vice), ResourceType = typeof(DisplayRes))]
        Vice,
        [Display(Name = nameof(DisplayRes.Teacher), ResourceType = typeof(DisplayRes))]
        Teacher,
        [Display(Name = nameof(DisplayRes.Assistant), ResourceType = typeof(DisplayRes))]
        Assistant
    }

    public class Teacher : Worker
    {
        [HiddenInput(DisplayValue = false)]
        public int TeacherId { get; set; }

        [Display(Name = nameof(DisplayRes.DegreeName), ResourceType = typeof(DisplayRes))]
        public string Degree { get; set; } // Научная степень    

        [Display(Name = nameof(DisplayRes.RankName), ResourceType = typeof(DisplayRes))]
        public Rank Rank { get; set; }

        [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressRes.EmailErrMsg), ErrorMessageResourceType = typeof(EmailAddressRes))]
        [Display(Name = nameof(DisplayRes.EmailName), ResourceType = typeof(DisplayRes))]
        public string Email { get; set; }

        [Display(Name = nameof(DisplayRes.SlugName), ResourceType = typeof(DisplayRes))]
        public string NameSlug { get; set; }

        [Display(Name = nameof(DisplayRes.Greetings), ResourceType = typeof(DisplayRes))]
        public string Greetings { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.IntellectName), ResourceType = typeof(DisplayRes))]
        public string Intellect { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.VkontakteName), ResourceType = typeof(DisplayRes))]
        public string Vk { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.FacebookName), ResourceType = typeof(DisplayRes))]
        public string Facebook { get; set; }

        [Url]
        [Display(Name = nameof(DisplayRes.TwitterName), ResourceType = typeof(DisplayRes))]
        public string Twitter { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool HasUser
        {
            get { return User != null; }
        }

        // PersonalPage

        [Display(Name = nameof(DisplayRes.DisciplinesName), ResourceType = typeof(DisplayRes))]
        public virtual ISet<Discipline> Disciplines { get; set; } = new SortedSet<Discipline>();

        [Display(Name = nameof(DisplayRes.ScienceInterestsName), ResourceType = typeof(DisplayRes))]
        public virtual ISet<ScienceInterest> ScienceInterests { get; set; } = new SortedSet<ScienceInterest>();

        [Display(Name = nameof(DisplayRes.ProjectsName), ResourceType = typeof(DisplayRes))]
        public virtual ISet<Project> Projects { get; set; } = new SortedSet<Project>();

        [Display(Name = nameof(DisplayRes.PublicationsName), ResourceType = typeof(DisplayRes))]
        public virtual ISet<Publication> Publications { get; set; } = new SortedSet<Publication>();

        public virtual Blog Blog { get; set; }

		public bool HasBlog { get { return Blog != null; } }

		public override string ToString()
		{
			return $"{FullName} ({GlobalRes.Teacher})";
		}
	}

    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            this.HasOptional(x => x.User)
                .WithOptionalPrincipal();
        }
    }
}
