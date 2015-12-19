using ACTS.Core.Abstract;
using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Concrete
{
	public class EFEmployeeRepository : IEmployeeRepository
	{
		private EFDbContext context = new EFDbContext();

		public IQueryable<Employee> Employees
		{
			get { return context.Employees; }
		}

		public Employee DeleteEmployee(int id)
		{
			throw new NotImplementedException();
		}

		public void SaveEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}
	}
}
