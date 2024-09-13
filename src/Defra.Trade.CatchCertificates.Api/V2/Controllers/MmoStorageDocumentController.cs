// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using CommonDtos = Defra.Trade.Common.Api.Dtos;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.V2.Controllers;

/// <summary>
/// Storage Documents received from MMO.
/// </summary>
[ApiVersion("2-internal")]
[ApiController]
[Route("mmo-storage-document")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoStorageDocumentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStorageDocumentRepository _repository;

    public MmoStorageDocumentController(IMapper mapper, IStorageDocumentRepository repository)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Updates or creates a V2 Storage Document using the Id and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="document">Storage Document details to be updated.</param>
    /// <response code="204">Storage Document has been created/updated.</response>
    /// <response code="400">The details of the Storage Document provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost(Name = "CreateStorageDocument")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] DtosMmo.StorageDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
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