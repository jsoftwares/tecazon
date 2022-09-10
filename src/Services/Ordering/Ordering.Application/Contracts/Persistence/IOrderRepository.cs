using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        //customer query method for the order repository in addition to methods it will inherit from IAsyncRepository
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
