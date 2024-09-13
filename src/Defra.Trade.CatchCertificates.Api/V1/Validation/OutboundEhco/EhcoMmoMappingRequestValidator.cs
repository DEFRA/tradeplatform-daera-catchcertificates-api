// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Linq;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.OutboundEhco
{
    public class EhcoMmoMappingRequestValidator : AbstractValidator<EhcoMmoMappingRequest>
    {
        public EhcoMmoMappingRequestValidator() : base()
        {
            RuleFor(x => x.MmoDocumentNumbers)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Count() <= 1).WithMessage("Only 1 document number is supported.");

            RuleForEach(x => x.MmoDocumentNumbers)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ResponseItems)
                .NotNull()
                .NotEmpty();
        }
    }
}
