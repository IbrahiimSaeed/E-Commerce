using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Stripe;
using Product = Domain.Entites.ProductModule.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites.OrderModule;
using AutoMapper;
using Domain.Entites.BasketModule;

namespace Services.Implementations
{
    internal class PaymentService(IConfiguration _configuration, IBasketRepository _basketRepository
        , IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        //public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        //{
        //    //0] Install stripe.net
        //    //1] Set up key [secret key]
        //    //2] Get basket [by basketId]
        //    //3] Validate Item price ==> [basket.item.price = product.price] ==> product from db
        //    //4] Validate Shipping Price ==> get deliveryMethod [DeliveryMethodId] ==> ShippingPrice = deliveryMethod.price
        //    //5] Total ==> [SubTotal + ShippingPrice] ==> cent ==> *100 ==> Long
        //    //6] Create Or Update paymentIntentId
        //    //7] Save changes [update] Basket
        //    //8] Map to basketDto ==> return
        //}
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];
            var basket = await GetBasketAsync(basketId);
            await ValidateBasketAsync(basket);
            var amount = CalculateTotalAsync(basket);
            await CreationOrUpdatePaymentIntentAsync(basket, amount);
            await _basketRepository.CreateOrUpdateAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            var stripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount, //total
                    Currency = "USD", //dollar
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount
                };
                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        private long CalculateTotalAsync(CustomerBasket basket)
        {
            var amount = (long)(basket.BasketItems.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;
            return amount;
        }

        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("no delivery method selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
        }

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);
        }
    }
}
