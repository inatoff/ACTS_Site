﻿using ACTS.Core.Concrete;
using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Identity
{
	public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
	{
		public ApplicationRoleStore(EFDbContext context)
			: base(context)
		{
		}
	}
}
