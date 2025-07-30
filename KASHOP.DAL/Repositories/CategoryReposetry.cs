using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories
{
    public class CategoryReposetry : ICategoryReposetry
    {
        private readonly ApplicationDbContext context;

        public CategoryReposetry(ApplicationDbContext context) {
            this.context = context;
        }

        public int Add(Category category)
        {
           context.categories.Add(category);
            return context.SaveChanges();

        }

        public int Delete(Category category)
        {
            context.categories.Remove(category);
            return context.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool WithTraking = false)
        {
           if(WithTraking == false)
            {
                return context.categories.AsNoTracking().ToList();
            }
            else
            {
                return context.categories.ToList();
            }
        }

        public Category? GetById(int id)
        {
            return context.categories.Find(id);
        }

        public int Update(Category category)
        {
            context.categories.Update(category);
            return context.SaveChanges();
        }
    }
}
