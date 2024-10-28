using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class DokumentValidator : AbstractValidator<DokumentDTO>
    {
        public DokumentValidator()
        {
            RuleFor(x => x.BrojDokumenta)
                .NotEmpty().WithMessage("Broj dokumenta je obavezan")
                .MaximumLength(50).WithMessage("Broj dokumenta ne sme biti duži od 50 karaktera");

            RuleFor(d => d.Komitent)
                .NotEmpty().WithMessage("Komitent je obavezan");

            RuleFor(x => x.DatumDospeca)
                .GreaterThan(x => x.DatumIzdavanja).WithMessage("Datum dospeća mora biti \n posle datuma izdavanja")
                .NotEmpty().WithMessage("Datum dospeća je obavezan");

        }
    }
}
