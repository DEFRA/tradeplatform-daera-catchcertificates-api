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
/// Processing statements received from MMO.
/// </summary>
[ApiVersion("1-internal")]
[ApiController]
[Route("mmo-processing-statement")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoProcessingStatementController(IMapper mapper, IProcessingStatementRepository repository) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IProcessingStatementRepository _repository = repository;

    /// <summary>
    /// Gets a paginated list of Processing Statement entries received from MMO using a search filter.
    /// </summary>
    /// <remarks>
    /// Sorting will be defaulted to Id ascending.
    /// </remarks>
    /// <param name="filter">Search filter and pagination parameters.</param>
    /// <response code="200">Successfully retrieved Processing Statements.</response>
    /// <response code="400">The provided search filter was invalid. Validation errors can be found in the response.</response>
    /// <returns>Paginated list of Processing Statements.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CommonDtos.PagedResult<DtosMmo.ProcessingStatementEntry>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] CommonDtos.PagedQuery filter)
    {
        var (certs, totalCount) = await _repository.GetPaginatedAsync(filter.PageNumber, filter.PageSize);

        var result = _mapper.Map<List<DtosMmo.ProcessingStatementEntry>>(certs);

        var response = new CommonDtos.PagedResult<DtosMmo.ProcessingStatementEntry>(result, filter, totalCount);

        return Ok(response);
    }

    /// <summary>
    /// Updates or creates a V1 Processing Statement using the Id and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="statement">Processing Statement details to be updated.</param>
    /// <response code="204">Processing Statement has been created/updated.</response>
    /// <response code="400">The details of the Processing Statement provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] DtosMmo.ProcessingStatement statement)
    {
        var existing = await _repository.GetByDocumentNumberAsync(statement.DocumentNumber);

        if (existing is null)
        {
            var dataRow = _mapper.Map<Models.ProcessingStatementDataRow>(statement);

            await _repository.CreateAsync(dataRow);
        }
        else
        {
            var existingTimestamp = existing.LastUpdated ?? existing.CreatedOn;

            if (statement.LastUpdated >= existingTimestamp)
            {
                existing = _mapper.Map(statement, existing);

                await _repository.UpdateAsync(existing);
            }
        }

        return NoContent();
    }
}