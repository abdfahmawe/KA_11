using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
   public interface ICategorySarvecies : IGenericServiece<CategoryRequistDTO, CategoryResponseDTO,Category>
    {
       
    }
}
