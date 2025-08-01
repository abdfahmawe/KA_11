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
   public interface IGenericServiece<TRequist ,TResponse , TEntity> where TEntity : BaseModel
    {
        int Create(TRequist requist);
        int Update(TRequist requist, int id);
        int Delete(int id);
        IEnumerable<TResponse> GetAll();
        TResponse? GetById(int id);
        public bool ToogleStatus(int id);
    }
}
