// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models.Enums;

namespace Defra.Trade.CatchCertificates.Api.Data;

public interface IEhcoMmoQuestionMappingRepository
{
    Task<IDictionary<long, MmoFieldMap>> GetQuestionMappings();
}