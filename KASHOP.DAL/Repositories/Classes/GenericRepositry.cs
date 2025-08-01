using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseModel
    {
        private readonly ApplicationDbContext context;

        public GenericRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public int Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool WithTraking = false)
        {
            if (WithTraking == false)
            {
                return context.Set<T>().AsNoTracking().ToList();
            }
            else
            {
                return context.Set<T>().ToList();
            }
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
