// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator(IValidator<ProductValidation> productValidationValidator) : base()
    {
        RuleFor(x => x.Id).NotNull();

        RuleFor(x => x.ForeignCatchCertificateNumber).NotNull();

        RuleFor(x => x.Species).NotNull();

        RuleFor(x => x.CnCode).NotNull();

        RuleFor(x => x.ImportedWeight).NotNull();

        RuleFor(x => x.ExportedWeight).NotNull();

        RuleFor(x => x.DateOfUnloading).NotNull();

        RuleFor(x => x.TransportUnloadedFrom).NotNull();

        RuleFor(x => x.Validation)
            .NotNull()
            .SetValidator(productValidationValidator);
    }
}