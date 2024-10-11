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
                .GreaterThan(0).WithMessage("Količina mora biti veća od 0");

            RuleFor(s => s.Roba!.Id)
                .GreaterThan(0).WithMessage("Roba je obavezna");
        }
    }
}
