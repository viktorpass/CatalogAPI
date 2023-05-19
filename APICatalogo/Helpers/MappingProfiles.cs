using APICatalogo.Dto;
using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.Helpers {
    public class MappingProfiles : Profile {
        public MappingProfiles() {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductsDto>();
        }
        
    }
}
