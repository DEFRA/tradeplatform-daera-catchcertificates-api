// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class CatchValidationValidatorTests
{
    private readonly CatchValidationValidator _sut;
    private readonly Fixture _fixture;

    public CatchValidationValidatorTests()
    {
        _sut = new();
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<CatchValidation>();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new CatchValidation();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Status)
            .WithErrorMessage("'Status' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.TotalUsedWeightAgainstCertificate)
            .WithErrorMessage("'Total Used Weight Against Certificate' must not be empty.");
        result.Errors.Should().HaveCount(2);
    }
}