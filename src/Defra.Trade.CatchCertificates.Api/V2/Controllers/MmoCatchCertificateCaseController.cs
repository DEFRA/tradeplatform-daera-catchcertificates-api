// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Defra.Trade.CatchCertificates.Api.V2.Controllers;

/// <summary>
/// Catch Certificate Cases received from MMO.
/// </summary>
[ApiVersion("2-internal")]
[ApiController]
[Route("mmo-catch-certificate-case")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoCatchCertificateCaseController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatchCertificateCaseRepository _repository;

    public MmoCatchCertificateCaseController(IMapper mapper, ICatchCertificateCaseRepository repository)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Updates or creates a V2 Catch Certificate Case using the document number and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="certificate">Catch Certificate Case details to be updated.</param>
    /// <response code="204">Catch Certificate Case has been created/updated.</response>
    /// <response code="400">The details of the Catch Certificate Case provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost(Name = "CreateCatchCertificateCase")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(Common.Api.Dtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] Dtos.Mmo.CatchCertificateCase certificate)
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