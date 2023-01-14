using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services;
using Ruper.BLL.Services.Contracts;
using Ruper.BLL.Validators.CategoryValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.BLL
{
    public static class BllServiceRegistration
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateDtoValidation>();

            return services;
        }
    }
}
