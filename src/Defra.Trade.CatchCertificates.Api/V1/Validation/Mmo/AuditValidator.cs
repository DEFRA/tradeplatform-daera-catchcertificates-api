// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class AuditValidator : AbstractValidator<Audit>
    {
        public AuditValidator() : base()
        {
            RuleFor(x => x.AuditAt).NotNull();

            RuleFor(x => x.AuditOperation).NotNull();

            RuleFor(x => x.User).NotNull();
        }
    }
}
