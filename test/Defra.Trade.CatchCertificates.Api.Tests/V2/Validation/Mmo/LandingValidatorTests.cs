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

public class LandingValidatorTests
{
    private readonly Mock<IValidator<LandingValidation>> _landingValidationValidator;
    private readonly LandingValidator _itemUnderTest;
    private readonly Fixture _fixture;

    public LandingValidatorTests()
    {
        _landingValidationValidator = new(MockBehavior.Strict);
        _itemUnderTest = new(_landingValidationValidator.Object);
        _fixture = new();
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        // arrange
        var model = _fixture.Create<Landing>();

        _landingValidationValidator.Setup(m => m.Validate(It.Is<ValidationContext<LandingValidation>>(ctx => ctx.InstanceToValidate == model.Validation)))
            .Returns(new ValidationResult())
            .Verifiable();

        // act
        var result = _itemUnderTest.TestValidate(model);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
        Mock.Verify(_landingValidationValidator);
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var model = new Landing();

        var result = _itemUnderTest.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("'Status' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Species)
            .WithErrorMessage("'Species' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CnCode)
            .WithErrorMessage("'Cn Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CommodityCodeDescription)
            .WithErrorMessage("'Commodity Code Description' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ScientificName)
            .WithErrorMessage("'Scientific Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.State)
            .WithErrorMessage("'State' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Presentation)
            .WithErrorMessage("'Presentation' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselName)
            .WithErrorMessage("'Vessel Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselPln)
            .WithErrorMessage("'Vessel Pln' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselLength)
            .WithErrorMessage("The specified condition was not met for 'Vessel Length'.");

        result.ShouldHaveValidationErrorFor(x => x.Weight)
            .WithErrorMessage("The specified condition was not met for 'Weight'.");

        result.ShouldHaveValidationErrorFor(x => x.NumberOfTotalSubmissions)
            .WithErrorMessage("The specified condition was not met for 'Number Of Total Submissions'.");

        result.ShouldHaveValidationErrorFor(x => x.Validation)
            .WithErrorMessage("'Validation' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselAdministration)
            .WithErrorMessage("'Vessel Administration' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Flag)
            .WithErrorMessage("'Flag' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.HomePort)
            .WithErrorMessage("'Home Port' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CatchArea)
            .WithErrorMessage("'Catch Area' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.FishingLicenceNumber)
            .WithErrorMessage("'Fishing Licence Number' must not be empty.");

        result.Errors.Should().HaveCount(19);

        Mock.Verify(_landingValidationValidator);
    }
}