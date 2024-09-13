// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class LandingValidationValidator : AbstractValidator<LandingValidation>
    {
        public LandingValidationValidator() : base()
        {
            RuleFor(x => x.LiveExportWeight).NotNull();

            RuleFor(x => x.SalesNoteUrl).NotNull();

            RuleFor(x => x.IsLegallyDue).NotNull();
        }
    }
}
