using AspnetMVC.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AspnetMVC
{
    public class ProductService : IProductService
    {
        private HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetById(int id)
        {
            var response = await _http.GetAsync($"http://localhost:5002/api/product/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAll()
        {
            var response = await _http.GetAsync("http://localhost:5002/api/product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAllCategory()
        {
            var response = await _http.GetAsync("http://localhost:5002/api/product/productcategory");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            var response = await _http.PutAsJsonAsync("http://localhost:5002/api/product", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateProduct(Product product)
        {
            var response = await _http.PostAsJsonAsync("http://localhost:5002/api/product", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetProductsByFiltration(QueryParams p)
        {

            var queryParams = HttpUtility.ParseQueryString(string.Empty);

            if (p.ProductCategoryId != 0)
                queryParams["ProductCategoryId"] = p.ProductCategoryId.ToString();

            if (!string.IsNullOrEmpty(p.OrderBy))
                queryParams["orderby"] = p.OrderBy;

            var queryString = queryParams.ToString();

            var response = await _http.GetAsync($"http://localhost:5002/api/product?{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

    }


}
