namespace ShedulerApp.Services
{
    public interface ITaskDbHandler
    {
        public Task UpdateLastExecutionInfo(int id);
        public Task UpdateStatsExecutionInfo(string username);
    }
}
