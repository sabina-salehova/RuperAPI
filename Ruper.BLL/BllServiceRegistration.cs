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
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateDtoValidation>();

            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IValidator<SliderCreateDto>, SliderCreateDtoValidation>();
            services.AddScoped<IValidator<SliderUpdateDto>, SliderUpdateDtoValidation>();

            return services;
        }
    }
}
