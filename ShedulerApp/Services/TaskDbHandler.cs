using Hangfire;
using Microsoft.EntityFrameworkCore;
using ShedulerApp.Models;

namespace ShedulerApp.Services
{
    public class TaskDbHandler : ITaskDbHandler
    { 
        private readonly CustomDbContext _context;

        public TaskDbHandler (CustomDbContext context)
        {
            _context = context;
        }

        public async Task UpdateLastExecutionInfo(int id)
        {
            var user = _context.Users
            .Where(c => c.TaskID == id)
            .FirstOrDefault();

            user.LastExecution = DateTime.UtcNow.ToString();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task UpdateStatsExecutionInfo(string username)
        {

            var stats = _context.AccountStats
            .Where(c => c.UserName == username)
            .FirstOrDefault();

            stats.LastExecution = DateTime.UtcNow.ToString();
            stats.TasksExecuted++;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
