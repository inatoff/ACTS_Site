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
        void SaveEmployee(Employee employee);
        Employee DeleteEmployee(int id);
    }
}
