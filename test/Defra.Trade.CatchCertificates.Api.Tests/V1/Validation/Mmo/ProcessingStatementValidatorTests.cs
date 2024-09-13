// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;
using FluentValidation.TestHelper;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V1.Validation.Mmo;

public class ProcessingStatementValidatorTests
{
    [Fact]
    public void Validate_Nulls_Error()
    {
        var minDate = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var itemUnderTest = new ProcessingStatementValidator();

        var result = itemUnderTest.TestValidate(new ProcessingStatement
        {
            LastUpdated = default(DateTimeOffset),
        });

        result.ShouldHaveValidationErrorFor(x => x.Exporter)
            .WithErrorMessage("'Exporter' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CaseType1)
            .WithErrorMessage("'Case Type1' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.CaseType2)
            .WithErrorMessage("'Case Type2' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.DocumentNumber)
            .WithErrorMessage("'Document Number' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdated)
            .WithErrorMessage($"'Last Updated' must be greater than or equal to '{minDate}'.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedBy)
            .WithErrorMessage("'Last Updated By' must not be empty.");

        result.ShouldHaveValidationErrorFor(x => x.LastUpdatedSystem)
            .WithErrorMessage("'Last Updated System' must not be empty.");
    }

    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ProcessingStatementValidator();

        var fixture = new Fixture();

        var result = itemUnderTest.TestValidate(
            fixture
                .Build<ProcessingStatement>()
                .With(x => x.LastUpdatedSystem, "TESTSYS")
                .Create());

        result.ShouldNotHaveAnyValidationErrors();
    }
}