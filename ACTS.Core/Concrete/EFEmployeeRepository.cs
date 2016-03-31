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
		private EFDbContext _context = new EFDbContext();

		public IQueryable<Employee> Employees
		{
			get { return _context.Employees; }
		}

		public void SaveEmployee(Employee employee)
		{
			if (employee.EmployeeId == 0)
			{
				_context.Employees.Add(employee);
			} else
			{
				Employee dbEntry = _context.Employees.Find(employee.EmployeeId);
				if (dbEntry != null)
				{
					dbEntry.FullName = employee.FullName;
					dbEntry.Position = employee.Position;
					dbEntry.Photo = employee.Photo;
					dbEntry.PhotoMimeType = employee.PhotoMimeType;
				}
			}

			_context.SaveChanges();
		}

		public Employee DeleteEmployee(int employeeId)
		{
			Employee dbEntry = _context.Employees.Find(employeeId);
			if (dbEntry != null)
			{
				_context.Employees.Remove(dbEntry);
				_context.SaveChanges();
			}
			return dbEntry;
		}

		public Employee GetEmployeeById(int employeeId)
		{
			return _context.Employees.Find(employeeId);
		}

		public void UpdateEmployee(Employee employee)
		{
			Employee dbEntry = _context.Employees.Find(employee.EmployeeId);
			if (dbEntry != null)
			{
				dbEntry.FullName = employee.FullName;
				dbEntry.Position = employee.Position;
				dbEntry.Photo = employee.Photo;
				dbEntry.PhotoMimeType = employee.PhotoMimeType;
			}

			_context.SaveChanges();
		}

		public void CreateEmployee(Employee employee)
		{
			_context.Employees.Add(employee);
			_context.SaveChanges();
		}
	}
}
