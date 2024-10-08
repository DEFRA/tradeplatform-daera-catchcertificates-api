// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Validation.Mmo;

public class ExporterValidatorTests
{
    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new ExporterValidator();

        var result = itemUnderTest.TestValidate(new Exporter());

        result.ShouldHaveValidationErrorFor(x => x.CompanyName)
            .WithErrorMessage("'Company Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage("'Address' must not be empty.");

        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ExporterValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Exporter>());

        result.ShouldNotHaveAnyValidationErrors();
    }
}