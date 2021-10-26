using EmployeeTest.Data;
using EmployeeTest.GenericRepo.GenericRepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTest.Data.Entity.EmployeeEntity;
using EmployeeTest.Service.EmployeeDataService.Interface;

namespace EmployeeTest.Service.EmployeeDataService
{

    public class EmployeeRepoService : GenericRepository<Employee>, IEmployeeRepoService
    {
        public EmployeeRepoService(EmployeeDbContext EmployeeDbContext) : base(EmployeeDbContext)
        {

        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var data = await GetAllAsync();
            return data.OrderByDescending(x=>x.Id);
        }
        public IEnumerable<Employee> GetAllEmployeeData()
        {
            return  GetAll();
        }
        public async Task<Employee> GetEmployee(int? id)
        {
            return await GetAsync(id);
        }

        public async Task<int> SaveEmployee(Employee model)
        {
            if (model.Id>0)
            {
                return await UpdateAsync(model,model.Id);
            }
            else
            {
                return await AddAsync(model);
            }
        }

        public async Task<bool> DeleteEmployee(Employee entityObj)
        {
            return await DeleteAsync(entityObj);
        }
    }
}
