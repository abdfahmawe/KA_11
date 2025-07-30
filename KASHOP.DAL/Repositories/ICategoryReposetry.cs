using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories
{
   public interface ICategoryReposetry
    {
        int Add(Category category);
        IEnumerable<Category> GetAll(bool WithTraking = false);
        Category? GetById(int id);
        int Update(Category category);
        int Delete(Category category);
    }
}
