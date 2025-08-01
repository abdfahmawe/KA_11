using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class BrandService : GenericService<BrandRequistDTO, BrandResponseDTO, Brand>, IBrandService
    {
        public BrandService(IGenericRepositry<Brand> GenericRepositry) : base(GenericRepositry)
        {
        }
    }
}
