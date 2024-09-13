// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class LandingValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new LandingValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Landing>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new LandingValidator();

        var result = itemUnderTest.TestValidate(new Landing());

        result.ShouldHaveValidationErrorFor(x => x.Status)
            .WithErrorMessage("'Status' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Species)
            .WithErrorMessage("'Species' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ScientificName)
            .WithErrorMessage("'Scientific Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CnCode)
            .WithErrorMessage("'Cn Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CommodityCodeDescription)
            .WithErrorMessage("'Commodity Code Description' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.State)
            .WithErrorMessage("'State' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Presentation)
            .WithErrorMessage("'Presentation' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselName)
            .WithErrorMessage("'Vessel Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.VesselPln)
            .WithErrorMessage("'Vessel Pln' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Validation)
            .WithErrorMessage("'Validation' must not be empty.");
    }
}