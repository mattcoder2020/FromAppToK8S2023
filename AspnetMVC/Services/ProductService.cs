using AspnetMVC.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AspnetMVC
{
    public class ProductService : IProductService
    {
        private HttpClient _http;
        private readonly IConfiguration configuration;
        private readonly String baseUrl;

        public ProductService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            this.configuration = configuration;
            //fetch a appsetting value with key "abc"
            baseUrl = this.configuration.GetSection("ProductServiceUrl").Value;
        }

        public async Task<string> GetById(int id)
        {
            
            var response = await _http.GetAsync(baseUrl+ $"/api/product/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAll()
        {
            var url = baseUrl + $"/api/product";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAllCategory()
        {
            var response = await _http.GetAsync(baseUrl + $"/api/product/productcategory");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            var response = await _http.PutAsJsonAsync(baseUrl + $"/api/product/", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateProduct(Product product)
        {
            var response = await _http.PostAsJsonAsync(baseUrl + $"/api/product/", product);
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

            var response = await _http.GetAsync(baseUrl+ $"/api/product/?{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

    }


}
