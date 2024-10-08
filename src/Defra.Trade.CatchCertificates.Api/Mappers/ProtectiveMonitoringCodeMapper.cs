// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.ProtectiveMonitoring.Mappers;
using AC = Defra.Trade.Common.Audit.Enums.TradeApiAuditCode;
using PMC = Defra.Trade.ProtectiveMonitoring.Models.ProtectiveMonitoringCode;

namespace Defra.Trade.CatchCertificates.Api.Mappers;

public class ProtectiveMonitoringCodeMapper : ProtectiveMonitoringMapperBase
{
    public ProtectiveMonitoringCodeMapper()
    {
        Map(AC.ProductionCatchCertificateCaseByDocNumber, PMC.BusinessTransactions);
        Map(AC.ProductionProcessingStatementByDocNumber, PMC.BusinessTransactions);
        Map(AC.ProductionStorageDocumentByDocNumber, PMC.BusinessTransactions);
    }
}