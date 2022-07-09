using AutoMapper;
using MediatR;
using Orders.Application.Contracts.Persistence;

namespace Orders.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<OrderDto>> Handle(
        GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _repository.GetOrdersByUserName(request.UserName);
        return _mapper.Map<List<OrderDto>>(orderList);
    }
}