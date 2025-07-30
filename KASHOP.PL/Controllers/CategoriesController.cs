using KASHOP.BLL.Services;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryServesices categoryServesices;

        public CategoriesController(CategoryServesices categoryServesices)
        {
            this.categoryServesices = categoryServesices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = categoryServesices.GetAllCategories();

            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult? GetById([FromRoute]int id)
        {
            var Category = categoryServesices.GetCategoryById(id);
            if (Category is null) return NotFound();
            return Ok(Category);

        }
        [HttpPost]
        public IActionResult Create( [FromBody] CategoryRequistDTO requist)
        {
           var id =  categoryServesices.CreateCategory(requist);
            return CreatedAtAction(nameof(GetById), new { id  });

        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] CategoryRequistDTO requist)
        {
            var updated = categoryServesices.UpdateCategory(requist, id);
            return updated>0?Ok() : NotFound();
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = categoryServesices.ToogleStatus(id);
            return updated == true ? Ok(new {message = "status toggled"}) : NotFound(new { message = "status not toggled" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
           var deleted= categoryServesices.DeleteCategory(id);
            return deleted > 0 ? Ok() : NotFound();
        }
    }
}
