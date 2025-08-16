using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
  public  class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Descount { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }

        // img 
        public string MainImage { get; set; }

        // relationships
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; } 
    }
}
