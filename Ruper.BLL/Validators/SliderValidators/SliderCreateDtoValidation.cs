using FluentValidation;
using Ruper.BLL.Dtos;
using System.Text.RegularExpressions;

namespace Ruper.BLL.Validators.CategoryValidators
{
    public class SliderCreateDtoValidation : AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValidation()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("title null ola bilmez")
                .NotEmpty().WithMessage("title daxil edilmelidir")
                .MinimumLength(3).WithMessage("title 3 simvoldan chox olmalidir")
                .MaximumLength(20).WithMessage("title 20 simvoldan az olmalidir");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("image null ola bilmez")
                .Must(file => file != null ? Regex.IsMatch(Path.GetExtension(file.FileName), "^.jpeg$|^.jpg$|^.png$") : false)
                .WithMessage("shekil jpeg, jpg ve ya png formatinda ola biler");
        }
    }
}
