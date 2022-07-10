using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orders.Application.Contracts.Infrastructure;
using Orders.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Orders.Infrastructure.EmailService;

public class SendGridEmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }
    public ILogger<SendGridEmailService> _logger { get; }

    public SendGridEmailService(IOptions<EmailSettings> emailSettings, ILogger<SendGridEmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }
    
    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation("Email is sent.");

        if (response.StatusCode is System.Net.HttpStatusCode.Accepted
            or System.Net.HttpStatusCode.OK)
            return true;

        _logger.LogError("Email sending failed.");
        
        return false;
    }
}