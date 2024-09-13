// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class CountryValidator : AbstractValidator<Dtos.Mmo.Country>
{
    public CountryValidator()
    {
        RuleFor(x => x.OfficialCountryName)
            .NotNull();

        RuleFor(x => x.IsoCodeAlpha2)
            .NotNull();

        RuleFor(x => x.IsoCodeAlpha3)
            .NotNull();

        RuleFor(x => x.IsoNumericCode)
            .NotNull();
    }
}