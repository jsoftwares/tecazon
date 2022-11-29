using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    //IRequest comes from the Mediator package
    /** According to Mediator implementation, every IRequest object must have a Handler class for the implementation:
     * in our case GetOrderListQueryHandler which will be triggerred from the mediator when d request comes.
     * It is best practice for CQRS pattern to separate our DTO (eg OrderVm) objects with their related commands & 
     * queries; this way we cam separate our dependencies with original entities; so we create all classes that 
     * belongs to a particular usecase (GetOrderList) under that use case to handle d request from the mediator object
     * The response type that IRequest takes shouls be the response type from this query
     * **/
    public class GetOrderListQuery : IRequest<List<OrdersVm>>
    {
        public string Username { get; set; }

        public GetOrderListQuery(string username)
        {
            Username = username;
        }
    }
}
