// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class LandingValidator : AbstractValidator<Landing>
{
    public LandingValidator(IValidator<LandingValidation> landingValidationValidator)
    {
        RuleFor(x => x.Status)
            .NotNull();

        RuleFor(x => x.Id)
            .NotNull();

        RuleFor(x => x.Species)
            .NotNull();

        RuleFor(x => x.CnCode)
            .NotNull();

        RuleFor(x => x.CommodityCodeDescription)
            .NotNull();

        RuleFor(x => x.ScientificName)
            .NotNull();

        RuleFor(x => x.State)
            .NotNull();

        RuleFor(x => x.Presentation)
            .NotNull();

        RuleFor(x => x.VesselName)
            .NotNull();

        RuleFor(x => x.VesselPln)
            .NotNull();

        RuleFor(x => x.VesselLength)
            .Must(x => x > 0);

        RuleFor(x => x.Weight)
            .Must(x => x > 0);

        RuleFor(x => x.NumberOfTotalSubmissions)
            .Must(x => x > 0);

        RuleFor(x => x.VesselAdministration)
            .NotNull();

        RuleFor(x => x.Flag)
            .NotNull();

        RuleFor(x => x.HomePort)
            .NotNull();

        RuleFor(x => x.CatchArea)
            .NotNull();

        RuleFor(x => x.FishingLicenceNumber)
            .NotNull();

        RuleFor(x => x.Validation)
            .NotNull()
            .SetValidator(landingValidationValidator);
    }
}