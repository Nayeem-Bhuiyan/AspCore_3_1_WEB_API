using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTest.Data.Entity.EmployeeEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTest.Data
{
    public class EmployeeDbContext : DbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        internal object tblName;

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }


        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));



            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).CreatedAt = DateTime.Now;

                }
                else
                {
                    entity.Property("CreatedAt").IsModified = false;
                    ((Base)entity.Entity).UpdatedAt = DateTime.Now;

                }

            }
        }
        #endregion

        #region EmployeeEntity
        public DbSet<Employee> Employee { get; set; }
        #endregion


    }
}

