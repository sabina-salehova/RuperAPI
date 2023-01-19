using AutoMapper;
using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;

namespace Ruper.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, BrandCreateDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryCreateDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryUpdateDto>().ReverseMap();

            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Slider, SliderCreateDto>().ReverseMap();
            CreateMap<Slider, SliderUpdateDto>().ReverseMap();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<Color, ColorCreateDto>().ReverseMap();
            CreateMap<Color, ColorUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
