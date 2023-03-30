using System;
using Common.DataAccess;
using Common.Model;
using OrderService.Models;
using OrderService.SQLiteDB;

namespace OrderService.Services
{
    internal class UnitOfWork<T>:IDisposable where T : ModelBase
    {
        private OrderDBContext dbContext;

        public UnitOfWork(OrderDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal void RegisterAddition(GenericSqlServerRepository<T, OrderDBContext> repo, Order order) 
        {
            throw new NotImplementedException();
        }

 
    }
}