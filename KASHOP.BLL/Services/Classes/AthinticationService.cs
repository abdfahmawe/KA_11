using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class AthinticationService : IAthinticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AthinticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserResponse> LoginAsync(LoginRequist loginRequist)
        {
            var user = await _userManager.FindByEmailAsync(loginRequist.Email);
            if (user == null)
            {
                throw new Exception("invalid email or password");
            }
           
                bool isPassed = await _userManager.CheckPasswordAsync(user, loginRequist.Password);
            if (!isPassed)
            {
                throw new Exception("invalid email or password");

            }
           return new UserResponse() { Token = await CreateTokenAsync(user) };
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtOptions")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.Name,user.FullName),
                  new Claim(ClaimTypes.NameIdentifier,user.Id),
                 
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public async Task<UserResponse> RegisterAsync(RegisterRequist registerRequist)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequist.UserName,
                Email = registerRequist.Email,
                FullName = registerRequist.FullName,
                PhoneNumber = registerRequist.PhoneNumber
            };
            //await _userManager.AddToRoleAsync(user, "Customer");
            var result =  await _userManager.CreateAsync(user, registerRequist.Password);
            
            if (result.Succeeded)
            {
                return new UserResponse()
                {
                    Token = registerRequist.Email,
                };
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                // نرمي الخطأ برسالة واضحة
                throw new Exception(errors);
            }
           
        }
    }
}
