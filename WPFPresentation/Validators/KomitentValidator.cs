using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class KomitentValidator : AbstractValidator<KomitentDTO>
    {
        public KomitentValidator()
        {
            RuleFor(x => x.SifraKomitenta)
                .NotEmpty().WithMessage("Sifra komitenta je obavezna");

            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan");

            RuleFor(x => x.Adresa)
                .NotEmpty().WithMessage("Adresa je obavezna");

            RuleFor(x => x.Grad)
                .NotEmpty().WithMessage("Grad je obavezan");
        }
    }
}
