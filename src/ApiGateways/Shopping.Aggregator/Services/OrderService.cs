using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        public Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
