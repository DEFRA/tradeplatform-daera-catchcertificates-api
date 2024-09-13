// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Threading.Tasks;
using Defra.Trade.Common.Audit.Enums;

namespace Defra.Trade.CatchCertificates.Api.Services
{
    public interface IProtectiveMonitoringService
    {
        Task LogSocEventAsync(TradeApiAuditCode auditCode, string message, string additionalInfo = "");
    }
}