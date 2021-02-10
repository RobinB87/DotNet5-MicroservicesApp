using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ApiCollection.Interfaces
{
    public interface IBasketApi
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket basket);
        Task<Basket> CheckoutBasket(BasketCheckout basket);
    }
}