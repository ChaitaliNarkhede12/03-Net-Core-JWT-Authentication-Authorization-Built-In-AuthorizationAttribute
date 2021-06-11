using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interface
{
    public interface IEmployeeAppService
    {
        Task<IEnumerable<Employee>> GetEmployeeList();
        Task<IEnumerable<Employee>> GetEmployeeById(Expression<Func<Employee, bool>> predicate);
        Employee GetEmployeeById(int id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee employee);
        Task<int> DeleteEmployee(int id);
    }
}
