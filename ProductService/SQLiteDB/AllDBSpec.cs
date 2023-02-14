using Common.DataAccess;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.SQLiteDB
{
    public class ProductByProductIdSpec : BaseSpecification<Product>
    {
        public ProductByProductIdSpec(int Id) : base(e => e.Id == Id)
        {}
    }

    public class ProductByNameSpec : BaseSpecification<Product>
    {
        public ProductByNameSpec(string productname) : base(e => e.Name == productname)
        { }
    }

    public class ProductIncludeCategory : BaseSpecification<Product>
    {
        public ProductIncludeCategory(): base(null)
        {
            base.AddIncludes(e => e.ProductCategory);
        }
    }
}
