using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class AthinticationService : IAthinticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AthinticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
           return new UserResponse() { Email = loginRequist.Email };
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
          var result =  await _userManager.CreateAsync(user, registerRequist.Password);
            if (result.Succeeded)
            {
                return new UserResponse()
                {
                    Email = registerRequist.Email,
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
