// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Threading.Tasks;
using Defra.Trade.Common.Audit.Enums;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api.Services
{
    public class ProtectiveMonitoringService(
        ILogger<ProtectiveMonitoringService> logger,
        ProtectiveMonitoring.Interfaces.IProtectiveMonitoringService socTradeProtectiveMonitoringService)
        : IProtectiveMonitoringService
    {
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private readonly ProtectiveMonitoring.Interfaces.IProtectiveMonitoringService _socProtectiveMonitoringService = socTradeProtectiveMonitoringService
                ?? throw new ArgumentNullException(nameof(socTradeProtectiveMonitoringService));

        /// <summary>
        /// Log event to SOC
        /// </summary>
        /// <param name="auditCode">Daera API audit code</param>
        /// <param name="message">Message to log</param>
        /// <param name="additionalInfo">Any additional information to log</param>
        /// <returns>Task</returns>
        public async Task LogSocEventAsync(TradeApiAuditCode auditCode, string message, string additionalInfo = "")
        {
            try
            {
                // Send event to Soc
                await _socProtectiveMonitoringService
                    .LogSocEventAsync(auditCode, message: message, additionalInfo: additionalInfo);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to send audit event for ProtectiveMonitoring.\r{Message}", ex.Message);
            }
        }
    }
}