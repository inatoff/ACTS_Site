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

namespace ACTS.Core.Entities
{
	 public class Teacher : Worker
	{
		public int TeacherId { get; set; }

		public string Degree { get; set; } // Научная степень    

		[EmailAddress]
		public string EMail { get; set; }
        
        [Required]
        public virtual string NameSlug { get; set; } //Дружественный ЮРЛ (по фамилии, в случае совпадения фамилии добавлять инициалы)

		//Social Links

		[Url]
		public string Intellect { get; set; }

		[Url]
		public string Vk { get; set; }

		[Url]
		public string FaceBook { get; set; }

		[Url]
		public string Twitter { get; set; }

		public virtual int? UserKey { get; set; }
		public virtual ApplicationUser User { get; set; }

		[NotMapped]
		public bool HasUser
		{
			get { return UserKey != null; }
		}

		// PersonalPage

        public virtual IList<string> Projects { get; set; }

        public virtual IList<string> Publications { get; set; }

        public virtual IList<string> ScienceInterests { get; set; }

        public virtual Blog Blog { get; set; }
	}

	public class TeacherMap : EntityTypeConfiguration<Teacher>
	{
		public TeacherMap()
		{
			this.HasOptional(x => x.User)
				.WithOptionalPrincipal()
				.Map(x => x.MapKey(nameof(Teacher.UserKey)));
		}
	}
}
