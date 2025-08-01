using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;


namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorySarvecies _categoryServesices;

        public CategoriesController(ICategorySarvecies _categoryServesices)
        {
            this._categoryServesices = _categoryServesices;
        }

        [HttpGet]
        public IActionResult GetAll() =>  Ok(_categoryServesices.GetAll());
        
        [HttpGet("{id}")]
        public IActionResult? GetById([FromRoute]int id)
        {
            var Category = _categoryServesices.GetById(id);
            if (Category is null) return NotFound();
            return Ok(Category);

        }
        [HttpPost]
        public IActionResult Create( [FromBody] CategoryRequistDTO requist)
        {
           var id =  _categoryServesices.Create(requist);
            return CreatedAtAction(nameof(GetById), new { id  } , new {message = requist});

        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] CategoryRequistDTO requist)
        {
            var updated = _categoryServesices.Update(requist, id);
            return updated>0?Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = _categoryServesices.ToogleStatus(id);
            return updated == true ? Ok(new {message = "status toggled"}) : NotFound(new { message = "status not toggled" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
           var deleted= _categoryServesices.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
