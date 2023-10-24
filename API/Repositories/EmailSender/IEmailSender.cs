using API.Models.DTOs;

namespace API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailDTO request);
    }
}
