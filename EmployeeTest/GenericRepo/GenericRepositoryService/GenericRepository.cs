using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeTest.Data;
using EmployeeTest.Data.Entity;
using EmployeeTest.GenericRepo.GenericRepositoryService.Interface;


namespace EmployeeTest.GenericRepo.GenericRepositoryService
{

    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {

        protected EmployeeDbContext _context;
        public GenericRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return  _context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T> GetAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(match);
        }


        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).AsNoTracking().ToListAsync();
        }



        public async Task<int> AddAsync(T Instance)
        {
            _context.Set<T>().Add(Instance);
           return await _context.SaveChangesAsync();

        }


        public async Task<int> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return 0;

            T existing = await _context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return key;
        }


        public async Task<bool> DeleteAsync(T Instance)
        {
            _context.Set<T>().Remove(Instance);
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().AsNoTracking().CountAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
