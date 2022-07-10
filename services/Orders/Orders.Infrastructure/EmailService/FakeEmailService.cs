using Microsoft.Extensions.Logging;
using Orders.Application.Contracts.Infrastructure;
using Orders.Application.Models;

namespace Orders.Infrastructure.EmailService;

public class FakeEmailService : IEmailService
{
    private readonly ILogger<FakeEmailService> _logger;
    
    public FakeEmailService(ILogger<FakeEmailService> logger)
    {
        _logger = logger;
    }
    
    public async Task<bool> SendEmail(Email email)
    {
        _logger.LogInformation($"Email is sent to {email.To}");
        return await Task.FromResult(true);
    }
}