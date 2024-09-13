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

public class CatchValidatorTests
{
    private readonly Mock<IValidator<CatchValidation>> _catchValidationValidator;
    private readonly CatchValidator _sut;
    private readonly Fixture _fixture;

    public CatchValidatorTests()
    {
        _catchValidationValidator = new(MockBehavior.Strict);
        _sut = new(_catchValidationValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<Catch>();
        _catchValidationValidator.Setup(m => m.Validate(It.Is<ValidationContext<CatchValidation>>(ctx => ctx.InstanceToValidate == model.Validation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_catchValidationValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new Catch();

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
        result.ShouldHaveValidationErrorFor(x => x.UsedWeightAgainstCertificate)
            .WithErrorMessage("'Used Weight Against Certificate' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.ProcessedWeight)
            .WithErrorMessage("'Processed Weight' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.Validation)
            .WithErrorMessage("'Validation' must not be empty.");
        result.Errors.Should().HaveCount(8);
        Mock.Verify(_catchValidationValidator);
    }
}