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

            CreateMap<ProductColor, ProductColorDto>().ReverseMap();
            CreateMap<ProductColor, ProductColorCreateDto>().ReverseMap();
            CreateMap<ProductColor, ProductColorUpdateDto>().ReverseMap();
            CreateMap<ProductColor, GeneralProductColorDto>().ReverseMap();

            CreateMap<Product, GeneralProductDto>().ReverseMap();

            CreateMap<ProductColorImage, PCIDto>().ReverseMap();
            CreateMap<ProductColorImage, GeneralPCIDto>().ReverseMap();
            CreateMap<ProductColorImage, PCICreateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserDto>().ReverseMap();

            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Rating, RatingCreateDto>().ReverseMap();
            CreateMap<RatingCreateDto, RatingWithoutUserDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<OrderCreateDto, OrderWithoutUserDto>().ReverseMap();
        }
    }
}
