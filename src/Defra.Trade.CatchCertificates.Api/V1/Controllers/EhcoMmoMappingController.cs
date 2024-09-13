// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Exceptions;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Api.Dtos;
using Defra.Trade.Common.Api.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DtosOutbound = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.V1.Controllers
{
    /// <summary>
    /// EHCO MMO Mapping.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("ehco-mmo-mapping")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EhcoMmoMappingController(IEhcoMmoMappingService service) : ControllerBase
    {
        private readonly IEhcoMmoMappingService _service = service;

        /// <summary>
        /// Map MMO data onto an EHCO question / answer set.
        /// </summary>
        /// <param name="mappingRequest">Mapping request context.</param>
        /// <response code="200">Successfully mapped from MMO Data onto EHCO definition.</response>
        /// <response code="400">The request provided failed validation checks.</response>
        /// <returns>The mapped EHCO response from the MMO documents specified.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(DtosOutbound.EhcoMmoMappedResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Process(DtosOutbound.EhcoMmoMappingRequest mappingRequest)
        {
            try
            {
                var result = await _service.ProcessMapping(mappingRequest);

                return Ok(result);
            }
            catch (MmoDocumentNotFoundException ex)
            {
                ModelState.AddModelError(
                    nameof(DtosOutbound.EhcoMmoMappingRequest.MmoDocumentNumbers).ToCamelCase(),
                    $"Document number '{ex.DocumentNumber}' is not a known Catch Certificate.");

                return ValidationProblem();
            }
        }
    }
}