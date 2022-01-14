using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
