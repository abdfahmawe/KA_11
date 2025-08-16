using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin ,Super_Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _Productservesices;

        public ProductsController(IProductService _Productservesices)
        {
            this._Productservesices = _Productservesices;
        }

        [HttpGet("")]

        public IActionResult GetAll() => Ok(_Productservesices.GetAll());

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] ProductRequist requist)
        {
            var Result = await _Productservesices.CreateWithFile(requist);
            return Ok(Result);
        }
    }
}
