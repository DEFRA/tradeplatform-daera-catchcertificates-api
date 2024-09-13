// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Enums;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class TransportationValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new TransportationValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Transportation>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error_When_ModeOfTransPort_Train()
    {
        var itemUnderTest = new TransportationValidator();

        var result = itemUnderTest.TestValidate(new Transportation { ModeOfTransport = ModeOfTransport.Train });

        result.ShouldHaveValidationErrorFor(x => x.BillNumber)
            .WithErrorMessage("'Bill Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ExportLocation)
            .WithErrorMessage("'Export Location' must not be empty.");

        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Validate_Nulls_Error_When_ModeOfTransPort_Plane()
    {
        var itemUnderTest = new TransportationValidator();

        var result = itemUnderTest.TestValidate(new Transportation { ModeOfTransport = ModeOfTransport.Plane });

        result.ShouldHaveValidationErrorFor(x => x.FlightNumber)
            .WithErrorMessage("'Flight Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ContainerId)
            .WithErrorMessage("'Container Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ExportLocation)
            .WithErrorMessage("'Export Location' must not be empty.");

        result.Errors.Should().HaveCount(3);
    }

    [Fact]
    public void Validate_Nulls_Error_When_ModeOfTransPort_Vessel()
    {
        var itemUnderTest = new TransportationValidator();

        var result = itemUnderTest.TestValidate(new Transportation { ModeOfTransport = ModeOfTransport.Vessel });

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("'Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Flag)
            .WithErrorMessage("'Flag' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ContainerId)
            .WithErrorMessage("'Container Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.ExportLocation)
            .WithErrorMessage("'Export Location' must not be empty.");

        result.Errors.Should().HaveCount(4);
    }
}