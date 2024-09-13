// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.OutboundEhco
{
    public class ApplicationFormItemValidator : AbstractValidator<ApplicationFormItem>
    {
        public ApplicationFormItemValidator() : base()
        {
        }
    }
}
