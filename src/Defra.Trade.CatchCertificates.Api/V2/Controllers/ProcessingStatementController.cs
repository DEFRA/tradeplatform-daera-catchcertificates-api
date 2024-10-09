// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Extensions;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Api.Dtos;
using Defra.Trade.Common.Api.OpenApi;
using Defra.Trade.Common.Audit.Enums;
using Defra.Trade.Common.ExternalApi.ApimIdentity;
using Defra.Trade.Common.ExternalApi.Auditing;
using Defra.Trade.Common.ExternalApi.Auditing.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using DtosOutbound = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

namespace Defra.Trade.CatchCertificates.Api.V2.Controllers;

/// <summary>
/// Processing Statements for consumers of MMO data.
/// </summary>
[ApiVersion("2")]
[ApiController]
[Route("processing-statement")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(ApimPassThroughSchemeOptions.Names.DaeraUserPolicy)]
public class ProcessingStatementController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProtectiveMonitoringService _protectiveMonitoringService;
    private readonly IProcessingStatementRepository _repository;
    private readonly ILogger<ProcessingStatementController> _logger;

    public ProcessingStatementController(
        IMapper mapper,
        IProcessingStatementRepository repository,
        IProtectiveMonitoringService protectiveMonitoringService,
        ILogger<ProcessingStatementController> logger)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(protectiveMonitoringService);
        ArgumentNullException.ThrowIfNull(logger);
        _mapper = mapper;
        _repository = repository;
        _protectiveMonitoringService = protectiveMonitoringService;
        _logger = logger;
    }

    /// <summary>
    /// Gets a V2 Processing Statement by document number.
    /// </summary>
    /// <param name="documentNumber">Document number for the Processing Statement.</param>
    /// <response code="200">Successfully retrieved the requested Processing Statement.</response>
    /// <response code="404">The specified Processing Statement does not exist.</response>
    /// <returns>The matching Processing Statement.</returns>
    [HttpGet("{documentNumber}")]
    [ProducesResponseType(typeof(DtosOutbound.ProcessingStatement), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CommonProblemDetailsNotFoundExample))]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status404NotFound)]
    [Audit(LogType = AuditLogType.DaeraFishExportServiceV2ProcessingStatementGetById, WithResponseBody = true, SystemRequestIdRouteName = "documentNumber")]
    public async Task<IActionResult> GetById([FromRoute] string documentNumber)
    {
        var dataRow = await _repository.GetByDocumentNumberAsync(documentNumber);

        if (dataRow is null)
        {
            _logger.ProcessingStatementGetByIdNotFound(documentNumber);

            return NotFound();
        }

        var statement = _mapper.Map<DtosMmo.ProcessingStatement>(dataRow);
        var result = _mapper.Map<DtosOutbound.ProcessingStatement>(statement);

        await _protectiveMonitoringService.LogSocEventAsync(
            TradeApiAuditCode.ProductionProcessingStatementByDocNumber,
            "Successfully fetched Processing Statement for Production");

        _logger.ProcessingStatementGetByIdSuccess(documentNumber);

        return Ok(result);
    }
}
