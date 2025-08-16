using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Requists
{
    public class ProductRequist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Descount { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }

        // img 
        public IFormFile MainImage { get; set; }

        // relationships
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
