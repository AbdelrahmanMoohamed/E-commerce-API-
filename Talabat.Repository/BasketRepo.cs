using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities;

namespace Talabat.Reposatory
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database =  redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await _database.StringGetAsync(BasketId);

            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);

            var CreatedOrUpdated = await _database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(1));

            if (!CreatedOrUpdated) return null;

            return await GetBasketAsync(Basket.Id);

        }
    }
}
