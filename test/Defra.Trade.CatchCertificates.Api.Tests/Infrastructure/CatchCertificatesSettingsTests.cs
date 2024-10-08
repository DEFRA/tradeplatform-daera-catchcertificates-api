// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.Infrastructure;

public class CatchCertificatesSettingsTests
{
    [Fact]
    public void Settings_ShouldBe_AsExpected()
    {
        // Act
        var sut = new CatchCertificatesSettings()
        {
            Sentinel = "Sentinel-mocked"
        };

        // Assert
        sut.Sentinel.Should().Be("Sentinel-mocked");
    }
}