using Microsoft.AspNetCore.Identity.UI.Services;

namespace MunicipalityManagementSystem.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly ILogger<EmailSender> _logger;

		public EmailSender(ILogger<EmailSender> logger)
		{
			_logger = logger;
		}

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			// For development: just log the email
			_logger.LogInformation("Email to {Email} with subject '{Subject}': {Message}", 
				email, subject, htmlMessage);
			
			// TODO: Implement actual email sending (e.g., using SendGrid, SMTP, etc.)
			return Task.CompletedTask;
		}
	}
}
