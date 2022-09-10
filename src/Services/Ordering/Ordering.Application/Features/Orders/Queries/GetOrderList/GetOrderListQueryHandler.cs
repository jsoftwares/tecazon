using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    //IRequestHandler takes d Request-Query and its Request response
    /** IOrderRepository is expected to be implemented in the infrastructure layer & will be registerred when the application 
     * starts up into d ASP.Net built-in dependency injection. This is why d Application layer only focuses on business
     * requirements and not d implementations(there's no implementation details from d DB related external systems here).
     * The biz requirement here is the customer needs to be able to get orders by username so we only implement
     * GetOrdersByUsername in our handler class**/
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersVm>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.Username);
            return _mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}
