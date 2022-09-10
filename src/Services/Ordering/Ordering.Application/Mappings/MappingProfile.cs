using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    // Mediator hleps us send command & query object to the correct Command/Query handler in clean architecture (Mediator design pattern) 
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
        }
    }
}
