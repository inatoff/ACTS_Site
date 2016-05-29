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

		[Obsolete]
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
			_context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
			_context.SaveChanges();
		}

		public void CreateEmployee(Employee employee)
		{
			_context.Employees.Add(employee);
			_context.SaveChanges();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFEmployeeRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
