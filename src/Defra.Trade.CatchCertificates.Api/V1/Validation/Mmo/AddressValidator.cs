// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator() : base()
        {
            RuleFor(x => x.Line1).NotNull();

            RuleFor(x => x.City).NotNull();

            RuleFor(x => x.PostCode).NotNull();
        }
    }
}
