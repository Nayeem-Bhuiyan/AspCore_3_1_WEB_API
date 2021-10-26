using EmployeeTest.Data.Entity.EmployeeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTest.Service.EmployeeDataService.Interface
{
   public interface IEmployeeRepoService
    {

        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int? id);
        Task<int> SaveEmployee(Employee model);
        Task<bool> DeleteEmployee(Employee entityObj);

        IEnumerable<Employee> GetAllEmployeeData();
    }
}
