using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    /**Now we create d OrderRepository which inherits from RepositoryBase; this is for d Order common operations. IOrderRepository
     * interface is to implement d custom queries specific for Order. Since this class inherits from RepositoryBase, we inherit all
     * the public&protected methods there hence we are able use all the basic CRUD methods in RepositoryBase. **/
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        /**We're adding this contructor here bcos when you have a base class as a constructor with a parameter, you have to add this
         * constructor parameter method with the sub classes; this is basic OOP principles**/
        public OrderRepository(OrderContext dbCcontext) : base(dbCcontext)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();

            return orderList;
        }
    }
}
