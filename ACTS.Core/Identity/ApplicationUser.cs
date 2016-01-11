using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Identity
{
	public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		public virtual int? TeacherKey { get; set; }
		public virtual Teacher Teacher { get; set; }

		[NotMapped]
		public bool HasTeacher
		{
			get{ return TeacherKey != null; }
		}
	}

	public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
	{
		public ApplicationUserMap()
		{
			this.HasOptional(x => x.Teacher)
				.WithOptionalPrincipal()
				.Map(x => x.MapKey(nameof(ApplicationUser.TeacherKey)));
		}
	}
}
