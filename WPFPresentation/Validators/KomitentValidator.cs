using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class KomitentValidator : AbstractValidator<KomitentDTO>
    {
        public KomitentValidator()
        {
            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan");

            RuleFor(x => x.Adresa)
                .NotEmpty().WithMessage("Adresa je obavezna");
        }
    }
}
