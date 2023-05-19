using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Dto {
    public class ProductsDto {
        public int ProductId { get; set; }
        public string? Name { get; set; }     
        public string? Description { get; set; }      
        public decimal Price { get; set; }
        public string? UrlImage { get; set; }
        public int CategoryId { get; set; }
    }
}
