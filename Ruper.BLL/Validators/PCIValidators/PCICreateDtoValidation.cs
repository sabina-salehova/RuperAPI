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
    public class PCICreateDtoValidation : AbstractValidator<PCICreateDto>
    {
        public PCICreateDtoValidation()
        {
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("image null ola bilmez")
                .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
                .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");

        }
    }
}
