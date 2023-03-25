using Common.DataAccess;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.SQLiteDB
{
    public class ProductIdSpec : BaseSpecification<Product>
    {
        public ProductIdSpec(int id) :base (e=>e.Id == id)
        { }
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
            base.AddIncludes(e => e.ProductCategoryId);
        }
    }

    //public class ProductByFiltrationSpec : BaseSpecification<Product>
    //{
    //    public ProductByFiltrationSpec(QueryParams queryParams) : 
    //        base(
    //            e=>(!queryParams.ProductCategoryId.HasValue || e.ProductCategoryId == queryParams.ProductCategoryId )
    //            && (!queryParams.Id.HasValue || e.Id == queryParams.Id)
    //            )
    //    {
    //        if (!String.IsNullOrEmpty(queryParams.OrderBy))
    //        {
    //            switch (queryParams.OrderBy)
    //            {
    //                case "name": base.AddSort(e => e.Name);
    //                    break;
    //                case "price_asc": base.AddSort(e => e.Price);
    //                    break;
    //                case "price_desc": base.AddSortDesc(e => e.Price);
    //                    break;
    //            }
    //        }
    //        base.AddIncludes(e => e.ProductCategory);
    //    }
    //}
}
