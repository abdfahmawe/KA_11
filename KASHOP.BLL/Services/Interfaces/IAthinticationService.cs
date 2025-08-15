using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
   public interface IAthinticationService
    {
        Task<UserResponse> LoginAsync(LoginRequist loginRequist);
        Task<UserResponse> RegisterAsync(RegisterRequist registerRequist);
        Task<ActionResult<string>> ConfirmEmail(string token, string UserId);
        Task<string> ResetPassword(ForgerPasswordRequist requist);

        Task<bool> ChangePassword(ChangePasswordRequist requist);
    }
}
