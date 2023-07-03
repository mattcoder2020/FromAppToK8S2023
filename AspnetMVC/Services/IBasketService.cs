using AspnetMVC.Models;

namespace AspnetMVC.Services
{
    public interface IBasketService
    {
        Task DeleteBasket();
        Task<Basket> GetBasket();
        BasketItem ProductToBasketItem(Product product);
        Task RemoveItemFromBasket(BasketItem item);
        Task SetBasket(Basket basket);
        Task<Basket> UpdateBasketItem(BasketItem basketItem);
        int GetBasketQuantity(Basket basket);
    }
}