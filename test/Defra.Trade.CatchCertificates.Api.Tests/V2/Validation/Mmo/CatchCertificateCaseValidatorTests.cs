// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

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

public class CatchCertificateCaseValidatorTests
{
    private readonly Mock<IValidator<Audit>> _auditValidator;
    private readonly Mock<IValidator<Country>> _countryValidator;
    private readonly Mock<IValidator<Exporter>> _exporterValidator;
    private readonly Fixture _fixture;
    private readonly CatchCertificateCaseValidator _itemUnderTest;
    private readonly Mock<IValidator<Landing>> _landingValidator;
    private readonly Mock<IValidator<Transportation>> _transportValidator;

    public CatchCertificateCaseValidatorTests()
    {
        _landingValidator = new(MockBehavior.Strict);
        _countryValidator = new(MockBehavior.Strict);
        _exporterValidator = new(MockBehavior.Strict);
        _auditValidator = new(MockBehavior.Strict);
        _transportValidator = new(MockBehavior.Strict);

        _itemUnderTest = new(_landingValidator.Object, _countryValidator.Object, _exporterValidator.Object, _auditValidator.Object, _transportValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_LastUpdated_Length_Error()
    {
        // arrange
        var model = _fixture.Build<CatchCertificateCase>()
            .With(x => x.Version, 2)
            .With(x => x.LastUpdatedSystem, new string('a', 11))
            .With(x => x.LastUpdatedBy, new string('z', 101))
            .Create();

        model.Exporter.FullName = null;

        foreach (var landing in model.Landings)
        {
            _landingValidator.Setup(m => m.Validate(It.Is<ValidationContext<Landing>>(ctx => ctx.InstanceToValidate == landing)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _countryValidator.Setup(m => m.Validate(It.Is<ValidationContext<Country>>(ctx => ctx.InstanceToValidate == model.ExportedTo)))
            .Returns(new ValidationResult())
            .Verifiable();

        _exporterValidator.Setup(m => m.Validate(It.Is<ValidationContext<Exporter>>(ctx => ctx.InstanceToValidate == model.Exporter)))
            .Returns(new ValidationResult())
            .Verifiable();

        foreach (var audit in model.Audits)
        {
            _auditValidator.Setup(m => m.Validate(It.Is<ValidationContext<Audit>>(ctx => ctx.InstanceToValidate == audit)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _transportValidator.Setup(m => m.Validate(It.Is<ValidationContext<Transportation>>(ctx => ctx.InstanceToValidate == model.Transportation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _itemUnderTest.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedSystem)
            .WithErrorMessage("'Last Updated System' must be between 1 and 10 characters. You entered 11 characters.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedBy)
            .WithErrorMessage("'Last Updated By' must be between 1 and 100 characters. You entered 101 characters.");

        result.ShouldHaveValidationErrorFor(x => x.Exporter.FullName)
            .WithErrorMessage("'Full Name' must not be empty.");

        result.Errors.Should().HaveCount(3);

        Mock.Verify(_landingValidator, _countryValidator, _exporterValidator, _auditValidator, _transportValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new CatchCertificateCase() { Version = 1 };

        // act
        var result = _itemUnderTest.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Version)
            .WithErrorMessage("The specified condition was not met for 'Version'.");

        result.ShouldHaveValidationErrorFor(x => x.Exporter)
            .WithErrorMessage("'Exporter' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ExportedTo)
            .WithErrorMessage("'Exported To' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CertStatus)
            .WithErrorMessage("'Cert Status' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CaseType1)
            .WithErrorMessage("'Case Type1' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CaseType2)
            .WithErrorMessage("'Case Type2' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.DocumentNumber)
            .WithErrorMessage("'Document Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.DA)
            .WithErrorMessage("'DA' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Transportation)
            .WithErrorMessage("'Transportation' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdated)
            .WithErrorMessage($"'Last Updated' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedBy)
            .WithErrorMessage("'Last Updated By' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedSystem)
            .WithErrorMessage("'Last Updated System' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.FailureIrrespectiveOfRisk)
            .WithErrorMessage("'Failure Irrespective Of Risk' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.MultiVesselSchedule)
            .WithErrorMessage("'Multi Vessel Schedule' must not be empty.");

        result.Errors.Should().HaveCount(14);

        Mock.Verify(_landingValidator, _countryValidator, _exporterValidator, _auditValidator, _transportValidator);
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Build<CatchCertificateCase>()
            .With(x => x.Version, 2)
            .With(x => x.LastUpdatedSystem, "TESTSYS")
            .Create();

        foreach (var landing in model.Landings)
        {
            _landingValidator.Setup(m => m.Validate(It.Is<ValidationContext<Landing>>(ctx => ctx.InstanceToValidate == landing)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _countryValidator.Setup(m => m.Validate(It.Is<ValidationContext<Country>>(ctx => ctx.InstanceToValidate == model.ExportedTo)))
            .Returns(new ValidationResult())
            .Verifiable();

        _exporterValidator.Setup(m => m.Validate(It.Is<ValidationContext<Exporter>>(ctx => ctx.InstanceToValidate == model.Exporter)))
            .Returns(new ValidationResult())
            .Verifiable();

        foreach (var audit in model.Audits)
        {
            _auditValidator.Setup(m => m.Validate(It.Is<ValidationContext<Audit>>(ctx => ctx.InstanceToValidate == audit)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _transportValidator.Setup(m => m.Validate(It.Is<ValidationContext<Transportation>>(ctx => ctx.InstanceToValidate == model.Transportation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _itemUnderTest.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_landingValidator, _countryValidator, _exporterValidator, _auditValidator, _transportValidator);
    }
}