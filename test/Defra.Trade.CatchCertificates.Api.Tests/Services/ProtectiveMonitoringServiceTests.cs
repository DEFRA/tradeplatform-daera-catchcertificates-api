// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Audit.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using protectiveMonitoring = Defra.Trade.ProtectiveMonitoring.Interfaces;

namespace Defra.Trade.CatchCertificates.Api.Tests.Services;

public class ProtectiveMonitoringServiceTests
{
    private readonly Mock<ILogger<ProtectiveMonitoringService>> _loggerMock;
    private readonly Mock<protectiveMonitoring.IProtectiveMonitoringService> _socProtectiveMonitoringServiceMock;
    private readonly ProtectiveMonitoringService _sut;

    public ProtectiveMonitoringServiceTests()
    {
        _loggerMock = new Mock<ILogger<ProtectiveMonitoringService>>();
        _socProtectiveMonitoringServiceMock = new Mock<protectiveMonitoring.IProtectiveMonitoringService>();
        _sut = GetDefaultSut();
    }

    [Fact]
    public async Task LogSocEvent_Ok()
    {
        _socProtectiveMonitoringServiceMock
            .Setup(r => r.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
                "Successfully fetched Catch Certificate case for Production - ok", string.Empty))
            .Returns(Task.CompletedTask);

        await _sut.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
            "Successfully fetched Catch Certificate case for Production - ok");

        _socProtectiveMonitoringServiceMock.Verify(
            r => r.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
                "Successfully fetched Catch Certificate case for Production - ok", string.Empty), Times.Once);
    }

    [Fact]
    public async Task LogSocEvent_Error()
    {
        _socProtectiveMonitoringServiceMock
            .Setup(r => r.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
                "Successfully fetched Catch Certificate case for Production - error", string.Empty))
            .Returns(Task.FromException(new Exception()));

        await _sut.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
            "Successfully fetched Catch Certificate case for Production - error");

        _socProtectiveMonitoringServiceMock.Verify(
            r => r.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber,
                "Successfully fetched Catch Certificate case for Production - error", string.Empty), Times.Once);
    }

    private ProtectiveMonitoringService GetDefaultSut() => new(
        _loggerMock.Object,
        _socProtectiveMonitoringServiceMock.Object);
}