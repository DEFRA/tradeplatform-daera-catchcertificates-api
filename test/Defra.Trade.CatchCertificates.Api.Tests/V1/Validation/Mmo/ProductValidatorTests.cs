// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class ProductValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ProductValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Product>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new ProductValidator();

        var result = itemUnderTest.TestValidate(new Product());

        result.ShouldHaveValidationErrorFor(x => x.ForeignCatchCertificateNumber)
            .WithErrorMessage("'Foreign Catch Certificate Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Species)
            .WithErrorMessage("'Species' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ScientificName)
            .WithErrorMessage("'Scientific Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CnCode)
            .WithErrorMessage("'Cn Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Validation)
            .WithErrorMessage("'Validation' must not be empty.");
    }
}