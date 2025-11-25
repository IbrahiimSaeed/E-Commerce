using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IOrderService
    {
        //GetById ==> Take Guid Id ==> Return OrderResult
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        //GetAllByEmail ==> Take String Email ==> Return IEnumerable<OrderResult>
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsunc(string userEmail);
        //CreateOrder ==> Take OrderRequest , String Email ==> Return OrderResult
        Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmail);
        //GetDeliveryMethod ==> Return IEnumerable<DeliveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
