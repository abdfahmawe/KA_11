using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
   public interface ICategorySarvecies
    {
        int CreateCategory(CategoryRequistDTO requist);
        int UpdateCategory(CategoryRequistDTO requist, int id);
        int DeleteCategory(int id);
        IEnumerable<CategoryResponseDTO>  GetAllCategories();
        CategoryResponseDTO? GetCategoryById(int id);
        public bool ToogleStatus(int id);
    }
}
