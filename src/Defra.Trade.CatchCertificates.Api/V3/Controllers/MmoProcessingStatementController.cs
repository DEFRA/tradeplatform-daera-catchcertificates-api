// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using CommonDtos = Defra.Trade.Common.Api.Dtos;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.V3.Controllers;

/// <summary>
/// Processing statements received from MMO.
/// </summary>
[ApiVersion("3-internal")]
[ApiController]
[Route("mmo-processing-statement")]
[Produces(MediaTypeNames.Application.Json)]
public class MmoProcessingStatementController : ControllerBase
{
    private readonly GenericMmoService<Dtos.Mmo.ProcessingStatement, ProcessingStatementDataRow> _mmoServcie;

    public MmoProcessingStatementController(
        GenericMmoService<Dtos.Mmo.ProcessingStatement, ProcessingStatementDataRow> mmoServcie)
    {
        ArgumentNullException.ThrowIfNull(mmoServcie);
        _mmoServcie = mmoServcie;
    }

    /// <summary>
    /// Updates or creates a V3 Processing Statement using the Id and details provided.
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
        await _mmoServcie.UpsertItem(statement);

        return NoContent();
    }
}
