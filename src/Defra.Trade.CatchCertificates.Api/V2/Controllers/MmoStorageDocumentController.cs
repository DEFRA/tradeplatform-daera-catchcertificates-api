// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Extensions;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private readonly GenericMmoService<Dtos.Mmo.StorageDocument, StorageDocumentDataRow> _mmoServcie;

    public MmoStorageDocumentController(
       GenericMmoService<Dtos.Mmo.StorageDocument, StorageDocumentDataRow> mmoServcie)
    {
        ArgumentNullException.ThrowIfNull(mmoServcie);
        _mmoServcie = mmoServcie;
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
        await _mmoServcie.UpsertItem(document);

        return NoContent();
    }
}
