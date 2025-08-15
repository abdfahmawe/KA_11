using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEmailSender _emailSender;

        public AthinticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration , IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task<UserResponse> LoginAsync(LoginRequist loginRequist)
        {
            var user = await _userManager.FindByEmailAsync(loginRequist.Email);
            if (user == null)
            {
                throw new Exception("invalid email or password");
            }
            if(! await _userManager.IsEmailConfirmedAsync(user) )
            {
                throw new Exception("Email is not confirmed yet, please check your email for confirmation link.");
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

        public async Task<ActionResult<string>> ConfirmEmail(string token, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
            {
                throw new Exception("User not Found ");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email Confirmed Successfully";
            }
            else
            {

                throw new Exception("email confirmation faild");
            }
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var urlEmail = $"https://localhost:7227/api/Identity/Account/ConfirmEmail?token={escapeToken}&UserId={user.Id}";

                await _emailSender.SendEmailAsync(user.Email,"Email Confirm" , $"<h1>Hello {user.UserName}</h1>"  + $"<a href={urlEmail}>Confirm Email</a>");
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


        public async Task<string> ResetPassword(ForgerPasswordRequist requist)
        {
            var user = await _userManager.FindByEmailAsync(requist.Email);
            if (user is null) throw new Exception("user not found");

            var random = new Random();
            var token = random.Next(1000,9999).ToString();

            await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"<h1>{token}</h1>");

            user.CodeResetPassword = token;
            user.CodeResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            await _userManager.UpdateAsync(user);

            return "Check your Email ";
        }

        public async Task<bool> ChangePassword(ChangePasswordRequist requist)
        {
            var user  = await _userManager.FindByEmailAsync(requist.Email);
            if(user is null) throw new Exception("user not found");

            if (user.CodeResetPassword != requist.token) {
                return false;
            }

            if(user.CodeResetPasswordExpiry < DateTime.Now)
            {
                return false; // Token expired
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
           var result = await _userManager.ResetPasswordAsync(user, token, requist.NewPassword);

            if(result.Succeeded)
          
                await _emailSender.SendEmailAsync(user.Email, "Password Changed", "<h1>Your password has been changed successfully.</h1>");

         return true;
            
           
        }
    }
}
