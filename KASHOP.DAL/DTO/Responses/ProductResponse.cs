using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
   public class ProductResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        public string ImgUrl => $"https://localhost:7227/Images/{MainImage}";
    }
}
