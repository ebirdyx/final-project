using AutoMapper;
using Orders.Application.Features.Orders.Commands.CheckoutOrder;
using Orders.Application.Features.Orders.Commands.UpdateOrder;
using Orders.Application.Features.Orders.Queries.GetOrdersList;
using Orders.Domain.Entities;

namespace Orders.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
    }
}