﻿using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client;
        }

        public async Task<BasketModel> GetBasket(string username)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{username}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
