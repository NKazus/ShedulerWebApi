using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ShedulerApp.Models;

namespace ShedulerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CustomDbContext _context;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(CustomDbContext context, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _authOptions = authOptions;
        }
        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await AuthenticateUser(login.Username, login.Password);
            if (user != null)
            {
                var token = GenerateGWT(user);
                return Ok(new
                {
                    access_token = token,
                    token_role = user.Role.ToString()
                }) ;
            }
            return Unauthorized();
        }

        private async Task<Account> AuthenticateUser(string username, string password)
        {
            return _context.Accounts
                .Where(c => c.UserName == username && c.Password == password)
                .FirstOrDefault();
        }

        private string GenerateGWT(Account user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim("UserName", user.UserName),
                new Claim("role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
