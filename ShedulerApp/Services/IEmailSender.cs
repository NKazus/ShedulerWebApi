namespace ShedulerApp.Services
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(int id, string username, int type, string parameter, string email);
    }
}
