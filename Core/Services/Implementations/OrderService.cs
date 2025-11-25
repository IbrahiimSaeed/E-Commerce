using AutoMapper;
using Domain.Contracts;
using Domain.Entites.OrderModule;
using Domain.Entites.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class OrderService(IMapper _mapper, IBasketRepository _basketRepository
        , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            var address = _mapper.Map<Address>(orderRequest.ShippingAddress);
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.BasketItems)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity));
            }
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);
            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResult>(orderToCreate);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethod);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithIncludeSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsunc(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderWithIncludeSpecifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
