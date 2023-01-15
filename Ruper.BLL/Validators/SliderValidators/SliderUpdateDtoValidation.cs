using FluentValidation;
using Ruper.BLL.Dtos;
using System.Text.RegularExpressions;

namespace Ruper.BLL.Validators.CategoryValidators
{
    public class SliderUpdateDtoValidation : AbstractValidator<SliderUpdateDto>
    {
        public SliderUpdateDtoValidation()
        {
            //RuleFor(x => x.Image)
            //    .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
            //    .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");
        }
    }
}
