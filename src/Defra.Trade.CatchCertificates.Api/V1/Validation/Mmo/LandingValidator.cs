// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class LandingValidator : AbstractValidator<Landing>
    {
        public LandingValidator() : base()
        {
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Species).NotNull();
            RuleFor(x => x.ScientificName).NotNull();
            RuleFor(x => x.CnCode).NotNull();
            RuleFor(x => x.CommodityCodeDescription).NotNull();
            RuleFor(x => x.LandingDate).NotNull();
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.Presentation).NotNull();
            RuleFor(x => x.VesselName).NotNull();
            RuleFor(x => x.VesselPln).NotNull();
            RuleFor(x => x.Weight).NotNull();
            RuleFor(x => x.NumberOfTotalSubmissions).NotNull();
            RuleFor(x => x.Validation).NotNull();
            RuleFor(x => x.Is14DayLimitReached).NotNull();
        }
    }
}