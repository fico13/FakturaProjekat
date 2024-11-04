using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class StavkaDokumentaValidator : AbstractValidator<StavkaDokumentaDTO>
    {
        public StavkaDokumentaValidator()
        {
            RuleFor(s => s.Kolicina)
                .NotEmpty().WithMessage("Količina je obavezna")
                .GreaterThan(0).WithMessage("Količina mora biti veća od 0")
                .LessThanOrEqualTo(s => Convert.ToInt32(s.Roba!.Stanje)).WithMessage("Nedovoljno robe na stanju");

            RuleFor(s => s.Roba!.SifraRobe)
                .NotEmpty().WithMessage("Roba je obavezna");
        }
    }
}
