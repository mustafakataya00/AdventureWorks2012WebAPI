using AdventureWorkAPI.Models;
using AdventureWorkAPI.Dto;
using AutoMapper;

namespace AdventureWorkAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<CategoryDto, ProductCategory>();

            CreateMap<InventoryDto, ProductInventory>();
            CreateMap<ProductInventory, InventoryDto>();

            CreateMap<ProductPhoto, ProductPhotoDto>();

            CreateMap<ProductSubcategory, SubCategoryDto>();
            CreateMap<SubCategoryDto, ProductSubcategory>();



        }

    }
}
