// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class AddressValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new AddressValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Address>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new AddressValidator();

        var result = itemUnderTest.TestValidate(new Address());

        result.ShouldHaveValidationErrorFor(x => x.Line1)
            .WithErrorMessage("'Line1' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage("'City' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.PostCode)
            .WithErrorMessage("'Post Code' must not be empty.");
    }
}