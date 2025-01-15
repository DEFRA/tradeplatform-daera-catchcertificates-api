// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class CountryValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new CountryValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Api.V2.Dtos.Mmo.Country>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new CountryValidator();

        var result = itemUnderTest.TestValidate(new Api.V2.Dtos.Mmo.Country());

        result.ShouldHaveValidationErrorFor(x => x.OfficialCountryName)
            .WithErrorMessage("'Official Country Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.IsoCodeAlpha2)
            .WithErrorMessage("'Iso Code Alpha2' must not be empty.");

        result.Errors.Should().HaveCount(2);
    }
}
