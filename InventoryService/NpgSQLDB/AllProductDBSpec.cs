using Common.DataAccess;
using InventoryService.Models;


namespace InventoryService.NpgSqlDB
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
           // base.AddIncludes(e => e.ProductCategoryId);
        }
    }

    public class ProductByCategoryIncludeCategorySpec : BaseSpecification<Product>
    {
        public ProductByCategoryIncludeCategorySpec(int categoryid) : base(e => e.ProductCategoryId == categoryid)
        {
           // base.AddIncludes(e => e.ProductCategoryId);
        }
    }

    public class ProductByIdSpec : BaseSpecification<Product>
    {
        public ProductByIdSpec(int id) : base(e => e.Id == id)
        {
           // base.AddIncludes(e => e.ProductCategoryId);
        }
    }
    public class ProductByFiltrationSpec : BaseSpecification<Product>
    {
        public ProductByFiltrationSpec(QueryParams queryParams) :
            base(
                e => 
                (!queryParams.ProductCategoryId.HasValue || e.ProductCategoryId == queryParams.ProductCategoryId)
                && 
                (!queryParams.ProductId.HasValue || e.Id == queryParams.ProductId)
                )
        {
          // base.AddIncludes(e => e.ProductCategoryId);
        }
    }
}
