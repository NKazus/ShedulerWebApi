namespace ShedulerApp.Services
{
    public static class EmailConfig
    {
        public static string From { get; } = "temp@gmail.com";
        public static string SmtpServer { get; } = "smtp.gmail.com";
        public static int Port { get; } = 465;
        public static string Username { get; } = "temp@gmail.com";
        public static string Password { get; } = "*********";
    }
}
