using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/**We will use this class to add/seed some prebuild Order data into the orders DB**/
namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                //We check that there are no orders in the Orders DB before we seed in data
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "jeffonochie", FirstName = "Jeffrey", LastName = "Onochie", EmailAddress = "jeff.ict@gmail.com", AddressLine = "Lekki, Lagos", Country = "Nigeria", TotalPrice = 350 }
            };
        }
    }
}
