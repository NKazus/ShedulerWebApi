using System.ComponentModel.DataAnnotations;


namespace ShedulerApp.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }

    public class AccountStats
    {
        [Key]
        public int AccountStatsId { get; set; }
        public string UserName { get; set; }

        public int TasksAdded { get; set; }
        public int TasksExecuted { get; set; }
        public int TasksDeleted { get; set; }
        public string LastExecution { get; set;}
    }
    public enum Role
    {
        User,
        Admin
    }
}
