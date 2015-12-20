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

		public void SaveEmployee(Employee employee)
		{
			if (employee.EmployeeID == 0)
			{
				context.Employees.Add(employee);
			} else
			{
				Employee dbEntry = context.Employees.Find(employee.EmployeeID);
				if (dbEntry != null)
				{
					dbEntry.FullName = employee.FullName;
					dbEntry.Position = employee.Position;
					dbEntry.Photo = employee.Photo;
					dbEntry.PhotoMimeType = employee.PhotoMimeType;
				}
			}

			context.SaveChanges();
		}

		public Employee DeleteEmployee(int employeeID)
		{
			Employee dbEntry = context.Employees.Find(employeeID);
			if (dbEntry != null)
			{
				context.Employees.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}
	}
}
