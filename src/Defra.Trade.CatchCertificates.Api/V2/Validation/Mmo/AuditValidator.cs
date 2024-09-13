// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class AuditValidator : AbstractValidator<Audit>
{
    public AuditValidator()
    {
        RuleFor(x => x.AuditOperation)
            .NotNull();

        RuleFor(x => x.User)
            .NotNull();

        RuleFor(x => x.AuditAt)
            .NotNull();
    }
}