using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
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

    }
}
