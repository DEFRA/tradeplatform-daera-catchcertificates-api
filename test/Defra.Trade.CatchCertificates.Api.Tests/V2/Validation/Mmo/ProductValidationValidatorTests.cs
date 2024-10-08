// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class ProductValidationValidatorTests
{
    private readonly ProductValidationValidator _sut;
    private readonly Fixture _fixture;

    public ProductValidationValidatorTests()
    {
        _sut = new();
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<ProductValidation>();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new ProductValidation();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Status)
            .WithErrorMessage("'Status' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.TotalWeightExported)
            .WithErrorMessage("'Total Weight Exported' must not be empty.");
        result.Errors.Should().HaveCount(2);
    }
}