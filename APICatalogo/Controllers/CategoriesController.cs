using APICatalogo.Context;
using APICatalogo.Dto;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace APICatalogo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryAndProducts() {

            try {
                var categoryAndProducts =  await _context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
                if(categoryAndProducts is null) {
                    return NotFound("Not found..");
                }

                var categoryAndProductsDto = _mapper.Map<List<CategoryDto>>(categoryAndProducts);
                return Ok(categoryAndProductsDto);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }  
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get() {
            try {
                var allCategories = await _context.Categories.AsNoTracking().ToListAsync();
                
                if(allCategories is null) {
                    return NotFound("Not found");
                }

                var allCategoriesDto = _mapper.Map<List<CategoryDto>>(allCategories);
                return Ok(allCategoriesDto);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
        }


        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> Get(int id) {
            try {
                var category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
                

                if (category == null) {
                    return NotFound("Category not found...");
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }           
        }


        [HttpPost]
        public ActionResult Post(Category category) {
            try {
                var categoryDto = _mapper.Map<CategoryDto>(category);

                if (category is null) {
                    return BadRequest();
                }
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = categoryDto.CategoryId }, categoryDto);
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
