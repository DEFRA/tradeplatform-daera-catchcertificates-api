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
/// Storage Documents received from MMO.
/// </summary>
[ApiVersion("1-internal")]
[ApiController]
[Route("mmo-storage-document")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoStorageDocumentController(IMapper mapper, IStorageDocumentRepository repository) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IStorageDocumentRepository _repository = repository;

    /// <summary>
    /// Gets a paginated list of Storage Document entries received from MMO using a search filter.
    /// </summary>
    /// <remarks>
    /// Sorting will be defaulted to Id ascending.
    /// </remarks>
    /// <param name="filter">Search filter and pagination parameters.</param>
    /// <response code="200">Successfully retrieved Storage Documents.</response>
    /// <response code="400">The provided search filter was invalid. Validation errors can be found in the response.</response>
    /// <returns>Paginated list of Storage Documents.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CommonDtos.PagedResult<DtosMmo.StorageDocumentEntry>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] CommonDtos.PagedQuery filter)
    {
        var (certs, totalCount) = await _repository.GetPaginatedAsync(filter.PageNumber, filter.PageSize);

        var result = _mapper.Map<List<DtosMmo.StorageDocumentEntry>>(certs);

        var response = new CommonDtos.PagedResult<DtosMmo.StorageDocumentEntry>(result, filter, totalCount);

        return Ok(response);
    }

    /// <summary>
    /// Updates or creates a V1 Storage Document using the Id and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="document">Storage Document details to be updated.</param>
    /// <response code="204">Storage Document has been created/updated.</response>
    /// <response code="400">The details of the Storage Document provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] DtosMmo.StorageDocument document)
    {
        var existing = await _repository.GetByDocumentNumberAsync(document.DocumentNumber);

        if (existing is null)
        {
            var dataRow = _mapper.Map<Models.StorageDocumentDataRow>(document);

            await _repository.CreateAsync(dataRow);
        }
        else
        {
            var existingTimestamp = existing.LastUpdated ?? existing.CreatedOn;

            if (document.LastUpdated >= existingTimestamp)
            {
                existing = _mapper.Map(document, existing);

                await _repository.UpdateAsync(existing);
            }
        }

        return NoContent();
    }
}