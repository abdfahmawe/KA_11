using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface IGenericRepositry<T> where T : BaseModel
    {
        int Add(T entity);
        IEnumerable<T> GetAll(bool WithTraking = false);
        T? GetById(int id);
        int Update(T entity);
        int Delete(T entity);
    }
}
