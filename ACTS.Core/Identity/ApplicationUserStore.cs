using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Identity
{
	public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int,
	ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		public ApplicationUserStore(EFDbContext context)
			: base(context)
		{
		}
	}
}
