using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {

        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
     
        public ActionResult<IEnumerable<Product>> Get() {
            try {
                var products = _context.Products.AsNoTracking().ToList();

                if (products is null) {
                    return NotFound("Products not found...");
                }

                return products;
            }
            catch (Exception e) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
            
        }

        [HttpGet("{id:int}", Name = "GetProduct")]

        public ActionResult<Product> Get(int id) {

            try {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product is null) {
                    return NotFound("Product not found...");
                }
                return product;
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
  

        }


        [HttpPost]
        public ActionResult Post(Product product) {

            try {
                if (product is null)
                    return BadRequest();

                _context.Products.Add(product);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetProduct",
                    new { id = product.ProductId }, product);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
            
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product) {
            try {
                if (id != product.ProductId) {
                    return BadRequest();
                }


                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }

            
        }



        [HttpDelete("{id:int}")]

        public ActionResult Delete(int id) {

            try {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

                if (product is null) {
                    return NotFound("Product not found.....");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
            


        }



    }
}
