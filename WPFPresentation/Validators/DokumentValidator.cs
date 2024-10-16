using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class DokumentValidator : AbstractValidator<DokumentDTO>
    {
        public DokumentValidator()
        {
            RuleFor(d => d.Komitent!.SifraKomitenta)
                .NotEmpty().WithMessage("Komitent je obavezan");

            RuleFor(d => d.Stavke!.Count)
                .GreaterThan(0).WithMessage("Stavke su obavezne");
        }
    }
}
