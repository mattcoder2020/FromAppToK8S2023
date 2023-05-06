﻿using Common.DataAccess;
using Common.Messages;
using InventoryService.Models;
using InventoryService.NpgSqlDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Service
{
    public class ProductService : IProductService
    {
        InventoryDBContext _dbcontext;
        public ProductService(InventoryDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Product[]> GetAll(ICorrelationContext context)
        {
            // var list =  await DataStore<Product>.GetInstance().GetRecords(i => i.Id == query.Id);
            var spec = new ProductIncludeCategorySpec();
            var repository = new GenericSqlServerRepository<Product, InventoryDBContext>(_dbcontext);
            IReadOnlyList<Product> products = await repository.GetEntityListBySpec(spec);
            return products.ToArray();
        }

        public async Task<Product[]> GetByFiltration(QueryParams productparams, ICorrelationContext context)
        {
            var spec = new ProductByFiltrationSpec(productparams);
            var repository = new GenericSqlServerRepository<Product, InventoryDBContext>(_dbcontext);
            IReadOnlyList<Product> products = await repository.GetEntityListBySpec(spec);
            return products.ToArray();
        }

        public async Task<Product[]> GetByCategoryId(int categoryid, ICorrelationContext context)
        {
            var spec = new ProductByCategoryIncludeCategorySpec(categoryid);
            var repository = new GenericSqlServerRepository<Product, InventoryDBContext>(_dbcontext);
            IReadOnlyList<Product> products = await repository.GetEntityListBySpec(spec);
            return products.ToArray();

        }

        public async Task<bool> PutByProductId(int productid, int quantity, ICorrelationContext context)
        {

            var spec = new ProductByIdSpec(productid);
            var repository = new GenericSqlServerRepository<Product, InventoryDBContext>(_dbcontext);
            Product product = await repository.GetEntityBySpec(spec);
            if (product != null)
            {
                product.Quantity = quantity;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
