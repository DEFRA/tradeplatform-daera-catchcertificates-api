// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
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
using Swashbuckle.AspNetCore.Filters;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using DtosOutbound = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

namespace Defra.Trade.CatchCertificates.Api.V2.Controllers;

/// <summary>
/// Catch Certificate Cases for consumers of MMO data.
/// </summary>
[ApiVersion("2")]
[ApiController]
[Route("catch-certificate-case")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(ApimPassThroughSchemeOptions.Names.DaeraUserPolicy)]
public class CatchCertificateCaseController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProtectiveMonitoringService _protectiveMonitoringService;
    private readonly ICatchCertificateCaseRepository _repository;

    public CatchCertificateCaseController(IMapper mapper, ICatchCertificateCaseRepository repository, IProtectiveMonitoringService protectiveMonitoringService)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(protectiveMonitoringService);
        _mapper = mapper;
        _repository = repository;
        _protectiveMonitoringService = protectiveMonitoringService;
    }

    /// <summary>
    /// Gets a V2 Catch Certificate Case by document number.
    /// </summary>
    /// <param name="documentNumber">Document number for the Catch Certificate Case.</param>
    /// <response code="200">Successfully retrieved the requested Catch Certificate Case.</response>
    /// <response code="404">The specified Catch Certificate Case does not exist.</response>
    /// <returns>The matching Catch Certificate Case.</returns>
    [HttpGet("{documentNumber}")]
    [ProducesResponseType(typeof(DtosOutbound.CatchCertificateCase), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CommonProblemDetailsNotFoundExample))]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status404NotFound)]
    [Audit(LogType = AuditLogType.DaeraFishExportServiceV2CatchCertificateCaseGetById, WithResponseBody = true, SystemRequestIdRouteName = "documentNumber")]
    public async Task<IActionResult> GetById([FromRoute] string documentNumber)
    {
        var dataRow = await _repository.GetByDocumentNumberAsync(documentNumber);

        if (dataRow is null)
        {
            return NotFound();
        }

        var certificate = _mapper.Map<DtosMmo.CatchCertificateCase>(dataRow);

        var result = _mapper.Map<DtosOutbound.CatchCertificateCase>(certificate);

        await _protectiveMonitoringService.LogSocEventAsync(TradeApiAuditCode.ProductionCatchCertificateCaseByDocNumber, "Successfully fetched Catch Certificate case for Production");

        return Ok(result);
    }
}