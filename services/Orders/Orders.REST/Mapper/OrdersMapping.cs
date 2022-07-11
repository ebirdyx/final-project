using AutoMapper;
using Messaging.Events;
using Orders.Application.Features.Orders.Commands.CheckoutOrder;

namespace Orders.REST.Mapper;

public class OrdersMapping : Profile
{
    public OrdersMapping()
    {
        CreateMap<CheckoutOrderCommand, CartCheckoutEvent>().ReverseMap();
    }
}