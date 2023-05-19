using APICatalogo.Models;
using System.Collections.ObjectModel;

namespace APICatalogo.Dto {
    public class CategoryDto {
        

        public int CategoryId { get; set; }

        public string? Name { get; set; }

        public string? UrlImage { get; set; }
        public ICollection<ProductsDto> Products { get; set; }
    }
}
