using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ACTS.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using ACTS.Core.Identity;
using ACTS.Core.Logging;

namespace ACTS.Core.Concrete
{
	public class EFDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int, 
		ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		// Employees -- наймити 
		public DbSet<Employee> Employees { get; set; }
		// Uncos -- новости в множественном числе
		public DbSet<News> Uncos { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
#if LogsInDb
		public DbSet<LogEntry> LogEntries { get; set; }
#endif

		public EFDbContext() : base("ACTSdbConnection")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new ApplicationUserMap());

			modelBuilder.Configurations.Add(new TeacherMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
