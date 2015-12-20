﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ACTS.Core.Entities;

namespace ACTS.Core.Concrete
{
	public class EFDbContext: DbContext
	{
		// Employees -- наймити 
		public DbSet<Employee> Employees { get; set; }
		// Uncos -- новости в множественном числе
		public DbSet<News> Uncos { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

		public EFDbContext() : base("ACTS.DB")
		{

		}
	}
}
