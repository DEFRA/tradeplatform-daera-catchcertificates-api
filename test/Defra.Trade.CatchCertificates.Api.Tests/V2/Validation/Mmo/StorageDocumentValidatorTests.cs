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

public class StorageDocumentValidatorTests
{
    private readonly Mock<IValidator<Product>> _productValidator;
    private readonly Mock<IValidator<Country>> _countryValidator;
    private readonly Mock<IValidator<Exporter>> _exporterValidator;
    private readonly Mock<IValidator<Authority>> _authorityValidator;
    private readonly Mock<IValidator<Transportation>> _transportValidator;
    private readonly StorageDocumentValidator _sut;
    private readonly Fixture _fixture;

    public StorageDocumentValidatorTests()
    {
        _productValidator = new(MockBehavior.Strict);
        _countryValidator = new(MockBehavior.Strict);
        _exporterValidator = new(MockBehavior.Strict);
        _authorityValidator = new(MockBehavior.Strict);
        _transportValidator = new(MockBehavior.Strict);
        _sut = new(_exporterValidator.Object, _productValidator.Object, _countryValidator.Object, _authorityValidator.Object, _transportValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Build<StorageDocument>()
            .With(x => x.Version, 2)
            .With(x => x.LastUpdatedSystem, "TESTSYS")
            .Create();

        _authorityValidator.Setup(m => m.Validate(It.Is<ValidationContext<Authority>>(ctx => ctx.InstanceToValidate == model.Authority)))
            .Returns(new ValidationResult())
            .Verifiable();

        foreach (var product in model.Products)
        {
            _productValidator.Setup(m => m.Validate(It.Is<ValidationContext<Product>>(ctx => ctx.InstanceToValidate == product)))
                .Returns(new ValidationResult())
                .Verifiable();
        }

        _countryValidator.Setup(m => m.Validate(It.Is<ValidationContext<Country>>(ctx => ctx.InstanceToValidate == model.ExportedTo)))
            .Returns(new ValidationResult())
            .Verifiable();

        _exporterValidator.Setup(m => m.Validate(It.Is<ValidationContext<Exporter>>(ctx => ctx.InstanceToValidate == model.Exporter)))
            .Returns(new ValidationResult())
            .Verifiable();

        _transportValidator.Setup(m => m.Validate(It.Is<ValidationContext<Transportation>>(ctx => ctx.InstanceToValidate == model.Transportation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_productValidator, _countryValidator, _exporterValidator, _authorityValidator, _transportValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        // arrange
        var model = new StorageDocument() { Version = 2 };

        // act
        var result = _sut.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Exporter)
            .WithErrorMessage("'Exporter' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.NumberOfFailedSubmissions)
            .WithErrorMessage("'Number Of Failed Submissions' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CaseType1)
            .WithErrorMessage("'Case Type1' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CaseType2)
            .WithErrorMessage("'Case Type2' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CompanyName)
            .WithErrorMessage("'Company Name' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.DocumentNumber)
            .WithErrorMessage("'Document Number' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdated)
            .WithErrorMessage("'Last Updated' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedBy)
            .WithErrorMessage("'Last Updated By' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedSystem)
            .WithErrorMessage("'Last Updated System' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.Products)
            .WithErrorMessage("'Products' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.DA)
            .WithErrorMessage("'DA' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.CorrelationId)
            .WithErrorMessage("'Correlation Id' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.RequestedByAdmin)
            .WithErrorMessage("'Requested By Admin' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.StorageFacilities)
            .WithErrorMessage("'Storage Facilities' must not be empty.");
        result.ShouldHaveValidationErrorFor(x => x.Authority)
            .WithErrorMessage("'Authority' must not be empty.");

        result.Errors.Should().HaveCount(15);
        Mock.Verify(_productValidator, _countryValidator, _exporterValidator, _authorityValidator, _transportValidator);
    }
}