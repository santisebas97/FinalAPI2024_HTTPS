using FinalAPI2024_HTTPS.Models;

namespace FinalAPI2024_HTTPS.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}
