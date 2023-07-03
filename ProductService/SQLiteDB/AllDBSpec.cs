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
        {
            base.AddIncludes(e => e.ProductCategory);
        }
    }

    public class ProductByNameSpec : BaseSpecification<Product>
    {
        public ProductByNameSpec(string productname) : base(e => e.Name == productname)
        { }
    }

    public class ProductIncludeCategorySpec : BaseSpecification<Product>
    {
        public ProductIncludeCategorySpec(): base(null)
        {
            base.AddIncludes(e => e.ProductCategory);
        }
    }

    public class ProductByFiltrationSpec : BaseSpecification<Product>
    {
        public ProductByFiltrationSpec(QueryParams queryParams) : 
            base(
                e=>(!queryParams.ProductCategoryId.HasValue || e.ProductCategoryId == queryParams.ProductCategoryId )
                && (!queryParams.Id.HasValue || e.Id == queryParams.Id)
                )
        {
            if (!String.IsNullOrEmpty(queryParams.OrderBy))
            {
                switch (queryParams.OrderBy)
                {
                    case "name": base.AddSort(e => e.Name);
                        break;
                    case "price_asc": base.AddSort(e => e.Price);
                        break;
                    case "price_desc": base.AddSortDesc(e => e.Price);
                        break;
                }
            }
            base.AddIncludes(e => e.ProductCategory);
        }
    }
}
