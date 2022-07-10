using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Contracts.Infrastructure;
using Orders.Application.Contracts.Persistence;
using Orders.Application.Models;
using Orders.Domain.Entities;

namespace Orders.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository repository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int> Handle(
        CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);
        var newOrder = await _repository.AddAsync(order);
        _logger.LogInformation($"Oder {newOrder.Id} is successfully created.");
        await SendMail(newOrder);
        return newOrder.Id;
    }

    private async Task SendMail(Order order)
    {
        var email = new Email
        {
            To = "noreply@orcuslab.com",
            Subject = $"Order {order.Id} was created",
            Body = "Order was created"
        };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {e.Message}");
        }
    }
}