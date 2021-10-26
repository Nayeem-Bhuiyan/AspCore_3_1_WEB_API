using EmployeeTest.Data;
using EmployeeTest.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeTest.GenericRepo.GenericRepositoryService.Interface
{

    public interface IGenericRepository<T> where T : Base
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> match);
        Task<int> AddAsync(T Instance);
        Task<int> UpdateAsync(T updated, int key);
        Task<bool> DeleteAsync(T Instance);
        Task<int> CountAsync();
    }
}
