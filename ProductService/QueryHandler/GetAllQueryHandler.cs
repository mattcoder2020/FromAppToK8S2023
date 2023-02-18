﻿using Common.DataAccess;
using Common.Handlers;
using Common.Messages;
using Common.Repo;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;
using ProductService.Query;
using ProductService.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductService.QueryHandler
{
    public class GetAllHandler : IQueryHandler<GetAllQuery, Product[]>
    {
        StoreDBContext _dbcontext;
        public GetAllHandler(StoreDBContext dbcontext)
        {
            //A repo implemenation can be defined as constructor param as DI approach 
            _dbcontext = dbcontext;
        }

        public async Task<Product[]> HandleAsync(GetAllQuery query, ICorrelationContext context)
        {
            // var list =  await DataStore<Product>.GetInstance().GetRecords(i => i.Id == query.Id);
            var spec = new ProductIncludeCategorySpec();
            var repository = new GenericSqlServerRepository<Product, StoreDBContext>(_dbcontext);
            IReadOnlyList<Product> products =  await repository.GetEntityListBySpec(spec);
            return products.ToArray();
            
        }
    }
}
