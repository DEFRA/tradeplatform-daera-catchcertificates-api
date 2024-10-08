// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class LandingValidationValidator : AbstractValidator<LandingValidation>
{
    public LandingValidationValidator()
    {
        RuleFor(x => x.LiveExportWeight)
            .Must(x => x > 0);

        RuleFor(x => x.SalesNoteUrl)
            .NotNull();
    }
}