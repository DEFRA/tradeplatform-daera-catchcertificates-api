// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

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
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutbound = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo;

namespace Defra.Trade.CatchCertificates.Api.V1.Controllers;

/// <summary>
/// Storage Documents for consumers of MMO data.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("storage-document")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(ApimPassThroughSchemeOptions.Names.DaeraUserPolicy)]
public class StorageDocumentController(
    IMapper mapper,
    IStorageDocumentRepository repository,
    IProtectiveMonitoringService protectiveMonitoringService) : ControllerBase
{
    private readonly IMapper _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    private readonly IProtectiveMonitoringService _protectiveMonitoringService = protectiveMonitoringService ?? throw new System.ArgumentNullException(nameof(protectiveMonitoringService));
    private readonly IStorageDocumentRepository _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));

    /// <summary>
    /// Gets a V1 Storage Document by document number.
    /// </summary>
    /// <param name="documentNumber">Document number for the Storage Document.</param>
    /// <response code="200">Successfully retrieved the requested Storage Document.</response>
    /// <response code="404">The specified Storage Document does not exist.</response>
    /// <returns>The matching Storage Document.</returns>
    [HttpGet("{documentNumber}")]
    [ProducesResponseType(typeof(DtosOutbound.StorageDocument), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(CommonProblemDetailsNotFoundExample))]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status404NotFound)]
    [Audit(LogType = AuditLogType.DaeraFishExportServiceV1StorageDocumentGetById, WithResponseBody = true, SystemRequestIdRouteName = "documentNumber")]
    public async Task<IActionResult> GetById([FromRoute] string documentNumber)
    {
        var dataRow = await _repository.GetByDocumentNumberAsync(documentNumber);

        if (dataRow is null)
        {
            return NotFound();
        }

        var document = _mapper.Map<DtosMmo.StorageDocument>(dataRow);

        var result = _mapper.Map<DtosOutbound.StorageDocument>(document);

        await _protectiveMonitoringService.LogSocEventAsync(TradeApiAuditCode.ProductionStorageDocumentByDocNumber, "Successfully fetched Storage Document for Production");

        return Ok(result);
    }
}