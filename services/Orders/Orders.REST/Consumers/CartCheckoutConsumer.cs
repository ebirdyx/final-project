using AutoMapper;
using MassTransit;
using MediatR;
using Messaging.Events;
using Orders.Application.Features.Orders.Commands.CheckoutOrder;

namespace Orders.REST.Consumers;

public class CartCheckoutConsumer : IConsumer<CartCheckoutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<CartCheckoutConsumer> _logger;

    public CartCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<CartCheckoutConsumer> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await _mediator.Send(command);

        _logger.LogInformation("Processed CartCheckoutEvent message. Created Order Id: {OrderId}", result);
    }
}