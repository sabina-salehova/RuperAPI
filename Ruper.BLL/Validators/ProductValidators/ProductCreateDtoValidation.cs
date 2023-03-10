using FluentValidation;
using Ruper.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ruper.BLL.Validators.CategoryValidators
{
    public class ProductCreateDtoValidation : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("name null ola bilmez")
                .NotEmpty().WithMessage("name daxil edilmelidir")
                .MinimumLength(3).WithMessage("name 3 simvoldan chox olmalidir")
                .MaximumLength(20).WithMessage("name 20 simvoldan az olmalidir");            
        }
    }
}
