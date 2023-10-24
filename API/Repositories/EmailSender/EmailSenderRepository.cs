using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using API.Models.DTOs;

namespace API.Services
{
    public class EmailSenderRepository : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSenderRepository(IConfiguration configuration) => this.configuration = configuration;
        
        public async Task SendEmailAsync(EmailDTO request)
        {
            string host = configuration["MailSettings:host"]!;
            int port = int.Parse(configuration["MailSettings:port"]!);
            string user = configuration["MailSettings:auth:user"]!;
            string password = configuration["MailSettings:auth:pass"]!;

            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse(user));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<p>{request.Body}</p>"};

            SmtpClient smtp = new();
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(user, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
