using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger;

        //Since we are getting EmailSettings from application setting, it should be IOption(using Microsoft.Extensions); this 
        //will help us get application settings in a structured way
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEMail(Application.Models.Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            //var from = new EmailAddress("hello@cbtexams.com", "Tecazon");
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _ - _emailSettings.FromName
            };

            var msg = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            _logger.LogInformation("Email sent!");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending falled");
            return false;
        }
    }
}
