﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class ProductValidationValidator : AbstractValidator<ProductValidation>
{
    public ProductValidationValidator() : base()
    {
        RuleFor(x => x.Status).NotNull();

        RuleFor(x => x.TotalWeightExported).NotNull();
    }
}
