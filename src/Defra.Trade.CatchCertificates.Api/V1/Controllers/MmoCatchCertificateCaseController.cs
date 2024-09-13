// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using CommonDtos = Defra.Trade.Common.Api.Dtos;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.V1.Controllers;

/// <summary>
/// Catch Certificate Cases received from MMO.
/// </summary>
[ApiVersion("1-internal")]
[ApiController]
[Route("mmo-catch-certificate-case")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoCatchCertificateCaseController(IMapper mapper, ICatchCertificateCaseRepository repository) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICatchCertificateCaseRepository _repository = repository;

    /// <summary>
    /// Gets a paginated list of Catch Certificate Case entries received from MMO using a search filter.
    /// </summary>
    /// <remarks>
    /// Sorting will be defaulted to Id ascending.
    /// </remarks>
    /// <param name="filter">Search filter and pagination parameters.</param>
    /// <response code="200">Successfully retrieved Catch Certificate Cases.</response>
    /// <response code="400">The provided search filter was invalid. Validation errors can be found in the response.</response>
    /// <returns>Paginated list of Catch Certificate Cases.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CommonDtos.PagedResult<DtosMmo.CatchCertificateCaseEntry>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] CommonDtos.PagedQuery filter)
    {
        var (certs, totalCount) = await _repository.GetPaginatedAsync(filter.PageNumber, filter.PageSize);

        var result = _mapper.Map<List<DtosMmo.CatchCertificateCaseEntry>>(certs);

        var response = new CommonDtos.PagedResult<DtosMmo.CatchCertificateCaseEntry>(result, filter, totalCount);

        return Ok(response);
    }

    /// <summary>
    /// Updates or creates a V1 Catch Certificate Case using the Id and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="certificate">Catch Certificate Case details to be updated.</param>
    /// <response code="204">Catch Certificate Case has been created/updated.</response>
    /// <response code="400">The details of the Catch Certificate Case provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] DtosMmo.CatchCertificateCase certificate)
    {
        var existing = await _repository.GetByDocumentNumberAsync(certificate.DocumentNumber);

        if (existing is null)
        {
            var dataRow = _mapper.Map<Models.CatchCertificateCaseDataRow>(certificate);

            await _repository.CreateAsync(dataRow);
        }
        else
        {
            var existingTimestamp = existing.LastUpdated ?? existing.CreatedOn;

            if (certificate.LastUpdated >= existingTimestamp)
            {
                existing = _mapper.Map(certificate, existing);

                await _repository.UpdateAsync(existing);
            }
        }

        return NoContent();
    }
}