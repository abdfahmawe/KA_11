using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
    public class CategoryReposetry : GenericRepositry<Category>, ICategoryReposetry
    {
        public CategoryReposetry(ApplicationDbContext context) : base(context)
        {

        }
    }
}
