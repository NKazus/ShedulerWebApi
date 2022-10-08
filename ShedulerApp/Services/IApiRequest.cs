namespace ShedulerApp.Services
{
    public interface IApiRequest
    {
        public Task ExecuteRequest(int type, string parameter);
        public string GetResponse();
    }
}
