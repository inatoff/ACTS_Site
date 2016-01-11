using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
		void UpdateEmployee(Employee employee);
		void CreateEmployee(Employee employee);
		Employee GetEmployeeById(int employeeId);
        Employee DeleteEmployee(int employeeID);
	}
}
