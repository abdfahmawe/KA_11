using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandServesices;

        public BrandsController(IBrandService _brandServesices)
        {
            this._brandServesices = _brandServesices;
        }

        [HttpGet]
        
        public IActionResult GetAll() => Ok(_brandServesices.GetAll(true));

        [HttpGet("{id}")]
        public IActionResult? GetById([FromRoute] int id)
        {
            var Category = _brandServesices.GetById(id);
            if (Category is null) return NotFound();
            return Ok(Category);

        }
    }
}
