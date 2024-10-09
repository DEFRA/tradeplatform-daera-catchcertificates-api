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
/// Storage Documents for consumers of MMO data.
/// </summary>
[ApiVersion("2")]
[ApiController]
[Route("storage-document")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(ApimPassThroughSchemeOptions.Names.DaeraUserPolicy)]
public class StorageDocumentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProtectiveMonitoringService _protectiveMonitoringService;
    private readonly IStorageDocumentRepository _repository;
    private readonly ILogger<StorageDocumentController> _logger;

    public StorageDocumentController(
        IMapper mapper,
        IStorageDocumentRepository repository,
        IProtectiveMonitoringService protectiveMonitoringService,
        ILogger<StorageDocumentController> logger)
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
    /// Gets a V2 Storage Document by document number.
    /// </summary>
    /// <param name="documentNumber">Document number for the Storage Document.</param>
    /// <response code="200">Successfully retrieved the requested Storage Document.</response>
    /// <response code="404">The specified Storage Document does not exist.</response>
    /// <returns>The matching Storage Document.</returns>
    [HttpGet("{documentNumber}")]
    [ProducesResponseType(typeof(DtosOutbound.StorageDocument), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CommonProblemDetailsNotFoundExample))]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status404NotFound)]
    [Audit(LogType = AuditLogType.DaeraFishExportServiceV2StorageDocumentGetById, WithResponseBody = true, SystemRequestIdRouteName = "documentNumber")]
    public async Task<IActionResult> GetById([FromRoute] string documentNumber)
    {
        var dataRow = await _repository.GetByDocumentNumberAsync(documentNumber);

        if (dataRow is null)
        {
            _logger.StorageDocumentGetByIdNotFound(documentNumber);

            return NotFound();
        }

        var document = _mapper.Map<DtosMmo.StorageDocument>(dataRow);
        var result = _mapper.Map<DtosOutbound.StorageDocument>(document);

        await _protectiveMonitoringService.LogSocEventAsync(
            TradeApiAuditCode.ProductionStorageDocumentByDocNumber,
            "Successfully fetched Storage Document for Production");

        _logger.StorageDocumentGetByIdSuccess(documentNumber);

        return Ok(result);
    }
}
