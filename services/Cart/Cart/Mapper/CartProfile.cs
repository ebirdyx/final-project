using AutoMapper;
using Cart.Controllers.v1;
using Messaging.Events;

namespace Cart.Mapper;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartCheckout, CartCheckoutEvent>().ReverseMap();
    }
}