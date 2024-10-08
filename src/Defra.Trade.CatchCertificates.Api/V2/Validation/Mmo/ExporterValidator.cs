// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class ExporterValidator : AbstractValidator<Exporter>
{
    public ExporterValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotNull();

        RuleFor(x => x.Address)
            .NotNull();
    }
}