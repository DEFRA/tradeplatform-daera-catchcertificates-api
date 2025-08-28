// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Extensions;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api.V2.Services;

public class MmoProcessingStatementService(IMapper mapper, IProcessingStatementRepository repository, ILogger<MmoProcessingStatementService> logger)
    : GenericMmoService<ProcessingStatement, ProcessingStatementDataRow>(mapper, repository, logger)
{
    protected override void LogCreateSuccess(string documentNumber) => _logger.MmoProcessingStatementCreateSuccess(documentNumber);
    protected override void LogUpdateSuccess(string documentNumber) => _logger.MmoProcessingStatementUpdateSuccess(documentNumber);
}
