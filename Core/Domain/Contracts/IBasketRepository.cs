using Domain.Entites.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Get basket by id
        Task<CustomerBasket?> GetBasketAsync(string id);
        //Create or Update basket
        Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
        //Delete basket
        Task<bool> DeleteBasketAsync(string id);
    }
}
