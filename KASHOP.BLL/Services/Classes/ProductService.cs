using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequist, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepositry _productRepositry;
        private readonly IFileService _fileService;

        public ProductService(IProductRepositry productRepositry , IFileService fileService) : base(productRepositry)
        {
            _productRepositry = productRepositry;
            _fileService = fileService;
        }

        public async Task<int> CreateWithFile(ProductRequist requist)
        {
            var entity = requist.Adapt<Product>();
            entity.CreatedAt = DateTime.Now;

            if (requist.MainImage != null)
            {
             var imagePath =   await _fileService.UploadFileAsync(requist.MainImage);
                entity.MainImage= imagePath;
            }
          return  _productRepositry.Add(entity);

        }
    }
}
