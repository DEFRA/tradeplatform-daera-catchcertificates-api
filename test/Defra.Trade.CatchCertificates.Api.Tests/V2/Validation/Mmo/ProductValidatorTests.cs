// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class ProductValidatorTests
{
    private readonly Mock<IValidator<ProductValidation>> _productValidationValidator;
    private readonly ProductValidator _sut;
    private readonly Fixture _fixture;

    public ProductValidatorTests()
    {
        _productValidationValidator = new(MockBehavior.Strict);
        _sut = new(_productValidationValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<Product>();

        _productValidationValidator.Setup(m => m.Validate(It.Is<ValidationContext<ProductValidation>>(ctx => ctx.InstanceToValidate == model.Validation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_productValidationValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new Product();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ForeignCatchCertificateNumber)
            .WithErrorMessage("'Foreign Catch Certificate Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Species)
            .WithErrorMessage("'Species' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CnCode)
            .WithErrorMessage("'Cn Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ImportedWeight)
            .WithErrorMessage("'Imported Weight' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ExportedWeight)
            .WithErrorMessage("'Exported Weight' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.DateOfUnloading)
            .WithErrorMessage("'Date Of Unloading' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.TransportUnloadedFrom)
            .WithErrorMessage("'Transport Unloaded From' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Validation)
            .WithErrorMessage("'Validation' must not be empty.");

        result.Errors.Should().HaveCount(9);

        Mock.Verify(_productValidationValidator);
    }
}