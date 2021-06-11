using ApplicationLayer.Interface;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class EmployeeAppService : IEmployeeAppService
    {
        public readonly IRepository<Employee> _employeeGenericRepository;
        public EmployeeAppService(IRepository<Employee> genericRepository)
        {
            _employeeGenericRepository = genericRepository;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeList()
        {

            var result = await _employeeGenericRepository.Get().ConfigureAwait(false);
            if (result == null)
            {
                throw new Exception("error");
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeById(Expression<Func<Employee, bool>> predicate)
        {
            var result = await _employeeGenericRepository.Get(predicate).ConfigureAwait(false);
            if (result == null)
            {
                throw new Exception("error");
            }
            return result;
        }

        public Employee GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("error");
            }

            var result = _employeeGenericRepository.GetById(id);

            if (result == null)
            {
                throw new Exception("error");
            }
            return result;
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new Exception("error");
            }

            await this._employeeGenericRepository.Add(employee);
            var result = await _employeeGenericRepository.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("error");
            }
            return result;
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            if (employee.EmpId <= 0)
            {
                throw new Exception("error");
            }

            this._employeeGenericRepository.Update(employee);
            var result = await _employeeGenericRepository.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("error");
            }
            return result;
        }

        public async Task<int> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new Exception("error");
            }

            var employee = _employeeGenericRepository.GetById(id);

            if (employee == null)
            {
                throw new Exception("error");
            }

            _employeeGenericRepository.Delete(employee);
            var result = await _employeeGenericRepository.SaveChangesAsync();

            if (result <= 0)
            {
                throw new Exception("error");
            }

            return result;

        }
    }
}
