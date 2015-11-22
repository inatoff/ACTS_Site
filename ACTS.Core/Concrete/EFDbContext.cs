using System;
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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
