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

public class ProcessingStatementValidatorTests
{
    private readonly Mock<IValidator<Address>> _addressValidator;
    private readonly Mock<IValidator<Authority>> _authorityValidator;
    private readonly Mock<IValidator<Catch>> _catchValidator;
    private readonly Mock<IValidator<Country>> _countryValidator;
    private readonly Mock<IValidator<Exporter>> _exporterValidator;
    private readonly Fixture _fixture;
    private readonly ProcessingStatementValidator _sut;

    public ProcessingStatementValidatorTests()
    {
        _catchValidator = new(MockBehavior.Strict);
        _countryValidator = new(MockBehavior.Strict);
        _exporterValidator = new(MockBehavior.Strict);
        _authorityValidator = new(MockBehavior.Strict);
        _addressValidator = new(MockBehavior.Strict);
        _sut = new(_catchValidator.Object, _countryValidator.Object, _exporterValidator.Object, _authorityValidator.Object, _addressValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new ProcessingStatement() { Version = 2 };

        // act
        var result = _sut.TestValidate(model);
        // assert
        result.ShouldHaveValidationErrorFor(x => x.Exporter)
            .WithErrorMessage("'Exporter' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CaseType1)
            .WithErrorMessage("'Case Type1' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CaseType2)
            .WithErrorMessage("'Case Type2' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CorrelationId)
            .WithErrorMessage("'Correlation Id' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.DA)
            .WithErrorMessage("'DA' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.DocumentNumber)
            .WithErrorMessage("'Document Number' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.HealthCertificateNumber)
            .WithErrorMessage("'Health Certificate Number' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.HealthCertificateDate)
            .WithErrorMessage("'Health Certificate Date' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.NumberOfFailedSubmissions)
            .WithErrorMessage("'Number Of Failed Submissions' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.PlantName)
            .WithErrorMessage("'Plant Name' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.PlantAddress)
            .WithErrorMessage("'Plant Address' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.PlantApprovalNumber)
            .WithErrorMessage("'Plant Approval Number' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.PlantDateOfAcceptance)
            .WithErrorMessage("'Plant Date Of Acceptance' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.RequestedByAdmin)
            .WithErrorMessage("'Requested By Admin' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.Authority)
            .WithErrorMessage("'Authority' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.PersonResponsible)
            .WithErrorMessage("'Person Responsible' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdated)
            .WithErrorMessage($"'Last Updated' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedBy)
            .WithErrorMessage("'Last Updated By' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedSystem)
            .WithErrorMessage("'Last Updated System' must not be empty.");

        result.Errors.Should().HaveCount(19);
        Mock.Verify(_catchValidator, _countryValidator, _exporterValidator, _authorityValidator, _addressValidator);
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Build<ProcessingStatement>()
            .With(x => x.Version, 2)
            .With(x => x.LastUpdatedSystem, "TESTSYS")
            .Create();

        _authorityValidator.Setup(m => m.Validate(It.Is<ValidationContext<Authority>>(ctx => ctx.InstanceToValidate == model.Authority)))
            .Returns(new ValidationResult())
            .Verifiable();

        foreach (var @catch in model.Catches)
        {
            _catchValidator.Setup(m => m.Validate(It.Is<ValidationContext<Catch>>(ctx => ctx.InstanceToValidate == @catch)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _countryValidator.Setup(m => m.Validate(It.Is<ValidationContext<Country>>(ctx => ctx.InstanceToValidate == model.ExportedTo)))
            .Returns(new ValidationResult())
            .Verifiable();

        _exporterValidator.Setup(m => m.Validate(It.Is<ValidationContext<Exporter>>(ctx => ctx.InstanceToValidate == model.Exporter)))
            .Returns(new ValidationResult())
            .Verifiable();

        _addressValidator.Setup(m => m.Validate(It.Is<ValidationContext<Address>>(ctx => ctx.InstanceToValidate == model.PlantAddress)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_catchValidator, _countryValidator, _exporterValidator, _authorityValidator, _addressValidator);
    }
}