// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models.Enums;
using Defra.Trade.Common.Sql.Data;
using Defra.Trade.Common.Sql.Infrastructure;

namespace Defra.Trade.CatchCertificates.Api.Data;

public class EhcoMmoQuestionMappingSqlRepository(IConnectionFactory connectionFactory)
    : RepositoryBase(connectionFactory), IEhcoMmoQuestionMappingRepository
{
    public async Task<IDictionary<long, MmoFieldMap>> GetQuestionMappings()
    {
        await Task.CompletedTask;

        var mappings = new Dictionary<long, MmoFieldMap>
        {
            { 142L, MmoFieldMap.DARegion }, //29594L
            { 164L, MmoFieldMap.ExporterFullNameCompanyName }, //29595L
            { 166L, MmoFieldMap.LandingSpecies }, //35897L
            { 167L, MmoFieldMap.LandingNetWeight }, //35904L
            { 165L, MmoFieldMap.ConsignmentNetWeight }, //35893L
            { 168L, MmoFieldMap.ExporterFullNameCompanyName }, //35909L
            { 169L, MmoFieldMap.ExporterAddress } //35910L
        };

        return mappings;
    }
}