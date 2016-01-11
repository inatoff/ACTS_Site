using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Identity
{
	public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
	{
		public ApplicationRole() { }
		public ApplicationRole(string name) { Name = name; }
	}
}
