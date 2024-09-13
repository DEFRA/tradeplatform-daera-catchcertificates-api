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

public class StorageFacilityValidatorTests
{
    private readonly Mock<IValidator<Address>> _addressValidator;
    private readonly StorageFacilityValidator _sut;
    private readonly Fixture _fixture;

    public StorageFacilityValidatorTests()
    {
        _addressValidator = new(MockBehavior.Strict);
        _sut = new(_addressValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<StorageFacility>();
        _addressValidator.Setup(m => m.Validate(It.Is<ValidationContext<Address>>(ctx => ctx.InstanceToValidate == model.Address)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_addressValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new StorageFacility();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage("'Address' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("'Name' must not be empty.");
        result.Errors.Should().HaveCount(2);
        Mock.Verify(_addressValidator);
    }
}