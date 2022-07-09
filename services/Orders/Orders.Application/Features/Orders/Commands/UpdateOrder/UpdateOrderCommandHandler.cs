using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Contracts.Persistence;
using Orders.Application.Exceptions;
using Orders.Application.Features.Orders.Commands.CheckoutOrder;
using Orders.Domain.Entities;

namespace Orders.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    
    public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<Unit> Handle(
        UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);
        if (order == null)
        {
            _logger.LogError($"Order {request.Id} does not exist on database.");
            throw new NotFoundException(nameof(Order), request.Id);
        }
        
        _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(Order));
        
        await _repository.UpdateAsync(order);
        
        _logger.LogInformation($"Order {order.Id} updated successfully.");
        
        return Unit.Value;
    }
}