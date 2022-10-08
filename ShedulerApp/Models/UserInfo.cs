using System.ComponentModel.DataAnnotations;

namespace ShedulerApp.Models
{
    public class UserInfo
    {
        [Key]
        public int TaskID { get; set; }
        public string UserName { get; set; }
        public int TaskType { get; set; }
        public string TaskParameter { get; set; }
        public string TaskName { get; set; }
        public string EmailAddress { get; set; }
        public string Cron { get; set; }
        public string LastExecution { get; set; }

    }
}
