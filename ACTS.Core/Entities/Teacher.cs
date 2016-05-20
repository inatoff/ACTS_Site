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
		// TODO: rustam переведи, а то я без понятия как это перевести
		Head,
		FirstVice,
		Vice,
		Teacher,
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
		[UIHint("OrderedByIntList")]
		public IList<Discipline> Disciplines { get; set; }

		[Display(Name = nameof(DisplayRes.ScienceInterestsName), ResourceType = typeof(DisplayRes))]
		[UIHint("OrderedByIntList")]
		public IList<ScienceInterest> ScienceInterests { get; set; }

		[Display(Name = nameof(DisplayRes.ProjectsName), ResourceType = typeof(DisplayRes))]
		[UIHint("OrderedByIntList")]
		public virtual IList<Project> Projects { get; set; }

		[Display(Name = nameof(DisplayRes.PublicationsName), ResourceType = typeof(DisplayRes))]
		[UIHint("OrderedByDateTimeList")]
		public virtual IList<Publication> Publications { get; set; }
		
		public virtual Blog Blog { get; set; }

		public bool HasBlog { get { return Blog != null; } }

		public Teacher()
		{
			Disciplines = new List<Discipline>();
			ScienceInterests = new List<ScienceInterest>();
			Projects = new List<Project>();
			Publications = new List<Publication>();
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
