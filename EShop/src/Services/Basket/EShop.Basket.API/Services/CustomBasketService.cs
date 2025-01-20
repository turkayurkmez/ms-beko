using EShop.Basket.API.Protos;
using Grpc.Core;

namespace EShop.Basket.API.Services
{
    public class CustomBasketService(ILogger<CustomBasketService> logger) : BasketService.BasketServiceBase
    {
        public override Task<BasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            //redis'den kullanıcının sepetini çek
            var response = new BasketResponse()
            {
                BuyerId = "test-user",
                Items = {
                       new BasketItem { ProductId = "1", ProductName = "Getting Product", Quantity = 5, Price = 100 }
                    }
            };
            return Task.FromResult(response);
        }

        public override Task<BasketResponse> AddItemToBasket(AddItemToBasketRequest request, ServerCallContext context)
        {
            var response = new BasketResponse()
            {
                BuyerId = "test-user",
                Items = {
                       new BasketItem { ProductId = "1", ProductName = "Added Product", Quantity = 5, Price = 100 }
                    }
            };
            return Task.FromResult(response);
        }

        public override Task<BasketResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
        {
            var response = new BasketResponse()
            {
                BuyerId = "test-user",
                Items = {
                       new BasketItem { ProductId = "1", ProductName = "Updated Product", Quantity = 5, Price = 100 }
                    }
            };

            return Task.FromResult(response);
        }
    }
}
