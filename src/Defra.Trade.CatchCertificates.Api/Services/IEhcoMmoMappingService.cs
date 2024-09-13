// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Services
{
    public interface IEhcoMmoMappingService
    {
        Task<EhcoMmoMappedResult> ProcessMapping(EhcoMmoMappingRequest mappingRequest);
    }
}