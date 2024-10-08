// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class AuthorityValidator : AbstractValidator<Authority>
{
    public AuthorityValidator(IValidator<Address> addressValidator) : base()
    {
        RuleFor(x => x.Name).NotNull();

        RuleFor(x => x.Address).SetValidator(addressValidator);

        RuleFor(x => x.CompanyName).NotNull();

        RuleFor(x => x.Address).NotNull();

        RuleFor(x => x.Tel).NotNull();

        RuleFor(x => x.Email).NotNull();

        RuleFor(x => x.DateIssued).NotNull();
    }
}