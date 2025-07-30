using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class CategoryServesices : ICategorySarvecies
    {
        private readonly ICategoryReposetry categoryReposetry;

        public CategoryServesices(ICategoryReposetry categoryReposetry ) {
            this.categoryReposetry = categoryReposetry;
        }
        public int CreateCategory(CategoryRequistDTO requist)
        {
            var category = requist.Adapt<Category>();
            return categoryReposetry.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = categoryReposetry.GetById(id);
            if (category is null) return 0;
             return categoryReposetry.Delete(category);
        }

        public IEnumerable<CategoryResponseDTO> GetAllCategories()
        {
            var categories = categoryReposetry.GetAll();
            return categories.Adapt<IEnumerable<CategoryResponseDTO>>();
        }

        public CategoryResponseDTO? GetCategoryById(int id)
        {
            var category = categoryReposetry.GetById(id);
            return category is null ? null : category.Adapt<CategoryResponseDTO>();
        }

        public int UpdateCategory(CategoryRequistDTO requist, int id)
        {
            var category = categoryReposetry.GetById(id);


            if (category is null) return 0;
            category.Name = requist.Name;
            return categoryReposetry.Update(category);
            
        }
        public bool ToogleStatus (int id)
        {
            var category = categoryReposetry.GetById(id);
            if (category is null) return false;
            category.status = category.status == Status.Active ? Status.Inactive : Status.Active;
            categoryReposetry.Update(category);
            return true;

        }
    }
}
