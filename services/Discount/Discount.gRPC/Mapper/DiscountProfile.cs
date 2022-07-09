using AutoMapper;
using Discount.Common.Entities;
using Discount.gRPC.Protos;

namespace Discount.gRPC.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}