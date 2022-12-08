using Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataAccess
{
    public class GenericSqlServerRepository<TEntity, TDbContext> 
        where TDbContext:DbContext
        where TEntity:ModelBase
    {
        DbContext _dbcontext;
        public GenericSqlServerRepository(TDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
            await _dbcontext.SaveChangesAsync();
        }

        //public async Task<bool> FindByPredicate(TEntity entity)
        //{
        //    await _dbcontext.Set<TEntity>().AddAsync(entity);
        //}

        public void DeleteModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
        }

        public void UpdateModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
        }

    }
}
