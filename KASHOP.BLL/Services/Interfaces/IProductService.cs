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
  public  interface IProductService : IGenericServiece<ProductRequist, ProductResponse, Product>
    {
        public Task<int> CreateWithFile(ProductRequist requist);
    }
}
