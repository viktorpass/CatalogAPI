using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace APICatalogo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context) {
            _context = context;
        }


        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoryAndProducts() {

            try {
                return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }  
        }


        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get() {
            try {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
        }


        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id) {
            try {
                var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

                if (category == null) {
                    return NotFound("Category not found...");
                }
                return Ok(category);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }           
        }


        [HttpPost]
        public ActionResult Post(Category category) {
            try {
                if (category is null) {
                    return BadRequest();
                }
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }    
        }


        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category) {
            try {
                if (id != category.CategoryId) {
                    return BadRequest("Id's don't match!");
                }
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
        }


        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id) {
            try {
                var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

                if (category is null) {
                    return NotFound("Category not found....");
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();

                return Ok(category);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
        }
    }
}
