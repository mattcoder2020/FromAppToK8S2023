using AspnetMVC.Models;
using Newtonsoft.Json;

namespace AspnetMVC.Services
{
    public class BasketService : IDisposable, IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _basketId;

        public BasketService(HttpClient http)
        {
            _httpClient = http;
            _baseUrl = "http://localhost:5002/api/basket/";
            _basketId = GetBasketId();
        }

        public async Task<Basket> GetBasket()
        {
            var response = await _httpClient.GetAsync(_baseUrl + _basketId);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Basket>(json);
        }

        public async Task SetBasket(Basket basket)
        {
            var json = JsonConvert.SerializeObject(basket);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBasket()
        {
            var response = await _httpClient.DeleteAsync(_baseUrl + _basketId);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Basket> UpdateBasketItem(BasketItem basketItem)
        {
            var basket = await GetBasket();
            if (basket == null)
            { 
                basket = new Basket();
                basket.basketid = GetBasketId();
               
            }

            var existingItem = basket.items.Find(item => item.id == basketItem.id);
            if (existingItem != null)
            {
                existingItem.quantity = existingItem.quantity + 1;
            }
            else
            {
                basketItem.quantity = 1;
                basket.items.Add(basketItem);
            }
            await SetBasket(basket);
            return basket;
        }

        public async Task RemoveItemFromBasket(BasketItem item)
        {
            var basket = await GetBasket();
            basket.items.RemoveAll(i => i.id == item.id);
            await SetBasket(basket);
        }

        public BasketItem ProductToBasketItem(Product product)
        {
            return new BasketItem
            {
                id = product.Id,
                name = product.Name,
                price = product.Price,
                productCategory = product.ProductCategory.Description,
                productCategoryId = product.ProductCategoryId,
                quantity = 0
            };
        }

        private string GetBasketId()
        {
            //write code to fetch cookie value


            var id = "matt_basket";
            //if (id == null)
            //{
            //    id = Guid.NewGuid().ToString();
            //    localStorage.setItem("basketid", id);
            //}
            return id;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public int GetBasketQuantity(Basket basket)
        {
            return basket.items.Sum<BasketItem>(e=>e.quantity);
        }

        

      
    }

}

