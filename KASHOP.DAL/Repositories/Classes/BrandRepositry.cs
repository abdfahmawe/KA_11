using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
   public class BrandRepositry : GenericRepositry<Brand> , IBrandRepositry
    {
        public BrandRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
