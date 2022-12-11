using Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<TEntity> FindByPrimaryKey(int id)
        {
            return await _dbcontext.Set<TEntity>().FindAsync(id);
        }

        public async Task DeleteModel(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
             await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateModel(TEntity entity)
        {
             _dbcontext.Set<TEntity>().Update(entity);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
