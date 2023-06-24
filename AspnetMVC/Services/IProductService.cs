using AspnetMVC.Models;

namespace AspnetMVC
{
    public interface IProductService
    {
        Task<string> CreateProduct(Product product);
        Task<string> GetAll();
        Task<string> GetAllCategory();
        Task<string> GetById(int id);
        Task<string> GetProductsByFiltration(QueryParams p);
        Task<string> UpdateProduct(Product product);
    }
}