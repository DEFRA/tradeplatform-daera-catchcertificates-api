// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class AuditValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new AuditValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(fixture.Create<Audit>());

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new AuditValidator();

        var result = itemUnderTest.TestValidate(new Audit());

        result.ShouldHaveValidationErrorFor(x => x.AuditOperation)
            .WithErrorMessage("'Audit Operation' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.User)
            .WithErrorMessage("'User' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.AuditAt)
            .WithErrorMessage("'Audit At' must not be empty.");
    }
}