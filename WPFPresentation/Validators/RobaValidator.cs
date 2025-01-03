﻿using Application.DTOs;
using FluentValidation;

namespace WPFPresentation.Validators
{
    public class RobaValidator : AbstractValidator<RobaDTO>
    {
        public RobaValidator()
        {
            RuleFor(x => x.SifraRobe)
                .NotEmpty().WithMessage("Šifra robe je obavezna");

            RuleFor(x => x.JedinicaMere)
                .NotEmpty().WithMessage("Jedinica mere je obavezna");

            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan");

            RuleFor(x => x.Cena)
                .NotEmpty().WithMessage("Cena je obavezna")
                .GreaterThan(0).WithMessage("Cena mora biti veća od 0");

            RuleFor(x => x.Stanje)
                .NotEmpty().WithMessage("Stanje je obavezno")
                .GreaterThanOrEqualTo(0).WithMessage("Stanje mora biti veće ili jednako 0");
        }
    }
}
