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
		public string Email { get; set; }

		//social Links

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

		public virtual int? UserKey { get; set; }
		public virtual ApplicationUser User { get; set; }

		[NotMapped]
		public bool HasUser
		{
			get { return UserKey != null; }
		}

		// Blog

		public virtual ICollection<Post> Blog { get; set; }
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
