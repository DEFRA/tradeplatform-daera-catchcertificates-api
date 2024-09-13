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

public class AuthorityValidatorTests
{
    private readonly Mock<IValidator<Address>> _addressValidator;
    private readonly Fixture _fixture;
    private readonly AuthorityValidator _sut;

    public AuthorityValidatorTests()
    {
        _addressValidator = new(MockBehavior.Strict);
        _sut = new(_addressValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new Authority();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorMessage("'Name' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.Address)
            .WithErrorMessage("'Address' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorMessage("'Email' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.DateIssued)
            .WithErrorMessage("'Date Issued' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.CompanyName)
            .WithErrorMessage("'Company Name' must not be empty.");
        result.ShouldHaveValidationErrorFor(m => m.Tel)
            .WithErrorMessage("'Tel' must not be empty.");
        result.Errors.Should().HaveCount(6);
        Mock.Verify(_addressValidator);
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<Authority>();

        _addressValidator.Setup(m => m.Validate(It.Is<ValidationContext<Address>>(ctx => ctx.InstanceToValidate == model.Address)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_addressValidator);
    }
}