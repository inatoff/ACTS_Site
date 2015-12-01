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
        IQueryable<Employee> HR { get; }
        void addEmployee(Employee employee);
        Employee getEmployee(int id);
        Employee editInfo(Employee employee);
        Employee deleteEmployee(int id);
    }
}
