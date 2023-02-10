using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services;
using Ruper.BLL.Services.Contracts;
using Ruper.BLL.Validators.CategoryValidators;

namespace Ruper.BLL
{
    public static class BllServiceRegistration
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IValidator<BrandCreateDto>, BrandCreateDtoValidation>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateDtoValidation>();

            services.AddScoped<ISubCategoryService, SubCategoryManager>();
            services.AddScoped<IValidator<SubCategoryCreateDto>, SubCategoryCreateDtoValidation>();

            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IValidator<SliderCreateDto>, SliderCreateDtoValidation>();
            services.AddScoped<IValidator<SliderUpdateDto>, SliderUpdateDtoValidation>();

            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<IValidator<ColorCreateDto>, ColorCreateDtoValidation>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateDtoValidation>();

            services.AddScoped<IProductColorService, ProductColorManager>();

            services.AddScoped<IPCIService, PCIManager>();
            services.AddScoped<IValidator<PCICreateDto>, PCICreateDtoValidation>();

            services.AddScoped<IRatingService, RatingManager>();

            return services;
        }
    }
}
