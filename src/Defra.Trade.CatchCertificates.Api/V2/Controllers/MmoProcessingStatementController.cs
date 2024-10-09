// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Extensions;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using CommonDtos = Defra.Trade.Common.Api.Dtos;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.V2.Controllers;

/// <summary>
/// Processing statements received from MMO.
/// </summary>
[ApiVersion("2-internal")]
[ApiController]
[Route("mmo-processing-statement")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoProcessingStatementController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProcessingStatementRepository _repository;
    private readonly ILogger<MmoProcessingStatementController> _logger;

    public MmoProcessingStatementController(
        IMapper mapper,
        IProcessingStatementRepository repository,
        ILogger<MmoProcessingStatementController> logger)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(logger);
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Updates or creates a V2 Processing Statement using the Id and details provided.
    /// </summary>
    /// <remarks>
    /// Specifically relates to the incoming data from MMO.
    /// </remarks>
    /// <param name="statement">Processing Statement details to be updated.</param>
    /// <response code="204">Processing Statement has been created/updated.</response>
    /// <response code="400">The details of the Processing Statement provided were invalid.</response>
    /// <returns>No content if the record is updated.</returns>
    [HttpPost(Name = "CreateProcessingStatement")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CommonProblemDetailsBadRequestExample))]
    [ProducesResponseType(typeof(CommonDtos.CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert([FromBody] DtosMmo.ProcessingStatement statement)
    {
        ArgumentNullException.ThrowIfNull(statement);
        var existing = await _repository.GetByDocumentNumberAsync(statement.DocumentNumber);

        if (existing is null)
        {
            var dataRow = _mapper.Map<Models.ProcessingStatementDataRow>(statement);

            await _repository.CreateAsync(dataRow);

            _logger.MmoProcessingStatementCreateSuccess(statement.DocumentNumber);
        }
        else
        {
            var existingTimestamp = existing.LastUpdated ?? existing.CreatedOn;

            if (statement.LastUpdated >= existingTimestamp)
            {
                existing = _mapper.Map(statement, existing);

                await _repository.UpdateAsync(existing);

                _logger.MmoProcessingStatementUpdateSuccess(statement.DocumentNumber);
            }
        }

        return NoContent();
    }
}
