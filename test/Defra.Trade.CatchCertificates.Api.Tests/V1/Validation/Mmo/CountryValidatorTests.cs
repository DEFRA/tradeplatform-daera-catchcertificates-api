// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class CountryValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new CountryValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Country>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new CountryValidator();

        var result = itemUnderTest.TestValidate(new Country());

        result.ShouldHaveValidationErrorFor(x => x.OfficialCountryName)
            .WithErrorMessage("'Official Country Name' must not be empty.");
    }
}