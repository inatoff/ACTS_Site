using ACTS.Core.Abstract;
using ACTS.Core.Identity;
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
	 public class Teacher : Worker
	{
		[HiddenInput(DisplayValue = false)]
		public int TeacherId { get; set; }

		public string Degree { get; set; } // Научная степень    

        public Rank Rank { get; set; }

		[EmailAddress]
		public string Email { get; set; }

        [Display(Name = "Slug")]
        public string NameSlug { get; set; }

		[Url]
		[Display(Name = "Intellect url")]
		public string Intellect { get; set; }

		[Url]
		[Display(Name = "Vkontakte url")]
		public string Vk { get; set; }

		[Url]
		[Display(Name = "Facebook url")]
		public string Facebook { get; set; }

		[Url]
		[Display(Name = "Twitter url")]
		public string Twitter { get; set; }

		public virtual ApplicationUser User { get; set; }

		public bool HasUser
		{
			get { return User != null; }
		}

		// PersonalPage

        public IList<string> Disciplines { get; set; }

        public IList<string> ScienceInterests { get; set; }

        public virtual IList<string> Projects { get; set; }

        public virtual IList<string> Publications { get; set; }
        
        public virtual Blog Blog { get; set; }
	}

	public class TeacherMap : EntityTypeConfiguration<Teacher>
	{
		public TeacherMap()
		{
			this.HasOptional(x => x.User)
				.WithOptionalPrincipal();
		}
	}

    public enum Rank
    {
        Head,
        FirstVice,
        Vice,
        Teacher,
        Assistant
    }
}
