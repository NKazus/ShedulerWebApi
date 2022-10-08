using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShedulerApp.Models;
using ShedulerApp.Services;

namespace ShedulerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly CustomDbContext _context;
        private string _username => User.Claims.Single(c => c.Type == "UserName").Value;

        public UserInfoController(CustomDbContext context)
        {
            this._context = context;
        }

       
        [HttpGet("stats")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AccountStats>>> GetUsersStats()
        {
            return await _context.AccountStats.ToListAsync();
        }

        // GET: api/UserInfo
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetSingleUser()
        {
            return await _context.Users
                .Where(c => c.UserName == _username)
                .ToListAsync();
        }

        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var userInfo = await _context.Users.FindAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            return userInfo;
        }


        // PUT: api/UserInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo userInfo)
        {
            if (id != userInfo.TaskID)
            {
                return BadRequest();
            }
            RecurringJob.AddOrUpdate<IEmailSender>($"task{userInfo.TaskID.ToString()}",
                (x) => x.SendEmailAsync(userInfo.TaskID, userInfo.UserName, userInfo.TaskType, userInfo.TaskParameter,
                userInfo.EmailAddress), userInfo.Cron);
            _context.Entry(userInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/UserInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //user

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
        {
            userInfo.UserName = _username;
            _context.Users.Add(userInfo);            
            await _context.SaveChangesAsync();
            RecurringJob.AddOrUpdate<IEmailSender>($"task{userInfo.TaskID.ToString()}", 
                (x) => x.SendEmailAsync(userInfo.TaskID, userInfo.UserName, userInfo.TaskType, userInfo.TaskParameter,
                userInfo.EmailAddress), userInfo.Cron);

            var userStat = _context.AccountStats
                .Where(c => c.UserName == _username)
                .FirstOrDefault();
            userStat.TasksAdded++;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserInfo", new { id = userInfo.TaskID }, userInfo);
        }

        // DELETE: api/UserInfo/5
        //user
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            var userInfo = await _context.Users.FindAsync(id);
            if (userInfo == null)
            {                
                return NotFound();
            }
            RecurringJob.RemoveIfExists($"task{userInfo.TaskID.ToString()}");
            _context.Users.Remove(userInfo);           
            await _context.SaveChangesAsync();
            var userStat = _context.AccountStats
                .Where(c => c.UserName == _username)
                .FirstOrDefault();
            userStat.TasksDeleted++;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserInfoExists(int id)
        {
            return _context.Users.Any(e => e.TaskID == id);
        }
    }
}
