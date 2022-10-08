using MailKit.Net.Smtp;
using MimeKit;

namespace ShedulerApp.Services
{
    public class EmailSender : IEmailSender
    {
        private string _emailFrom;
        private string _emailServer;
        private int _emailPort;
        private string _emailUser;
        private string _emailPassword;

        private readonly ITaskDbHandler _dbHandler;
        private readonly IApiRequest _apiRequest;

        public EmailSender(ITaskDbHandler dbHandler, IApiRequest apiRequest)
        {
            _emailFrom = EmailConfig.From;
            _emailServer = EmailConfig.SmtpServer;
            _emailPort = EmailConfig.Port;
            _emailUser = EmailConfig.Username;
            _emailPassword = EmailConfig.Password;

            _dbHandler = dbHandler;
            _apiRequest = apiRequest;
        }
        public async Task SendEmailAsync(int id, string username, int type, string parameter, string email)
        {
            await _apiRequest.ExecuteRequest(type, parameter);

            var emailMessage = CreateEmailMessage(_apiRequest.GetResponse(), email);

            await SendEmailMessageAsync(emailMessage);

            //update task info
            await _dbHandler.UpdateLastExecutionInfo(id);
            await _dbHandler.UpdateStatsExecutionInfo(username);
        }

        private MimeMessage CreateEmailMessage(string data, string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Host", _emailFrom));
            message.To.Add(new MailboxAddress("Recipient", email));
            message.Subject = "API_DATA";
            var emailBody = new BodyBuilder();
            var dataStream = new StreamGenerator();
            emailBody.Attachments.Add("Data.csv", dataStream.GenerateStreamFromString(data));
            message.Body = emailBody.ToMessageBody();
            return message;
        }

        private async Task SendEmailMessageAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailServer, _emailPort,true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailUser,_emailPassword);

                    await client.SendAsync(message);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
