// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class StorageFacilityValidator : AbstractValidator<StorageFacility>
{
    public StorageFacilityValidator(IValidator<Address> addressValidator) : base()
    {
        RuleFor(x => x.Address).NotNull().SetValidator(addressValidator);

        RuleFor(x => x.Name).NotNull();
    }
}

