// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class ProductValidationValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ProductValidationValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<ProductValidation>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new ProductValidationValidator();

        var result = itemUnderTest.TestValidate(new ProductValidation());

        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("'Status' must not be empty.");
    }
}