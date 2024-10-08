// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class LandingValidationValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new LandingValidationValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<LandingValidation>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new LandingValidationValidator();

        var result = itemUnderTest.TestValidate(new LandingValidation());

        result.ShouldHaveValidationErrorFor(x => x.LiveExportWeight)
            .WithErrorMessage("The specified condition was not met for 'Live Export Weight'.");

        result.ShouldHaveValidationErrorFor(x => x.SalesNoteUrl)
            .WithErrorMessage("'Sales Note Url' must not be empty.");

        result.Errors.Should().HaveCount(2);
    }
}