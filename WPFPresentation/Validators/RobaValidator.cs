using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class RobaValidator : AbstractValidator<RobaDTO>
    {
        public RobaValidator()
        {
            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan");

            RuleFor(x => x.Cena)
                .NotEmpty().WithMessage("Cena je obavezna")
                .GreaterThan(0).WithMessage("Cena mora biti veća od 0");
        }
    }
}
