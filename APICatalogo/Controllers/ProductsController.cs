using APICatalogo.Context;
using APICatalogo.Dto;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalogo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
     
        public async Task<ActionResult<IEnumerable<Product>>> Get() {
            try {
                var products = await _context.Products.AsNoTracking().ToListAsync();
                

                if (products is null) {
                    return NotFound("Products not found...");
                }
                var productsDto = _mapper.Map<List<ProductsDto>>(products);
                return Ok(productsDto);

            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred during the request");
            }
            
        }

        [HttpGet("{id:int}", Name = "GetProduct")]

        public async Task<ActionResult<Product>> Get(int id) {

            try {
                var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
                if (product is null) {
                    return NotFound("Product not found...");
                }
                var productDto = _mapper.Map<ProductsDto>(product);
                return Ok(productDto);
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

                var productDto = _mapper.Map<ProductsDto>(product);

                return new CreatedAtRouteResult("GetProduct",
                    new { id = productDto.ProductId }, productDto);
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
