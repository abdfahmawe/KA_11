using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;


namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandServesices;

        public BrandsController(IBrandService _brandServesices)
        {
            this._brandServesices = _brandServesices;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll() =>  Ok(_brandServesices.GetAll());
        
        [HttpGet("{id}")]
        public IActionResult? GetById([FromRoute]int id)
        {
            var Category = _brandServesices.GetById(id);
            if (Category is null) return NotFound();
            return Ok(Category);

        }
        [HttpPost]
        public IActionResult Create( [FromBody] BrandRequistDTO requist)
        {
           var id = _brandServesices.Create(requist);
            return CreatedAtAction(nameof(GetById), new { id  } , new {message = requist});

        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] BrandRequistDTO requist)
        {
            var updated = _brandServesices.Update(requist, id);
            return updated>0?Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = _brandServesices.ToogleStatus(id);
            return updated == true ? Ok(new {message = "status toggled"}) : NotFound(new { message = "status not toggled" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
           var deleted= _brandServesices.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
