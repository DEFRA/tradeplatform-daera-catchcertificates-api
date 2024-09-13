// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class CatchValidator : AbstractValidator<Catch>
    {
        public CatchValidator() : base()
        {
            RuleFor(x => x.ForeignCatchCertificateNumber).NotNull();

            RuleFor(x => x.Species).NotNull();

            RuleFor(x => x.ScientificName).NotNull();

            RuleFor(x => x.CnCode).NotNull();

            RuleFor(x => x.ImportedWeight).NotNull();

            RuleFor(x => x.UsedWeightAgainstCertificate).NotNull();

            RuleFor(x => x.Validation).NotNull();
        }
    }
}