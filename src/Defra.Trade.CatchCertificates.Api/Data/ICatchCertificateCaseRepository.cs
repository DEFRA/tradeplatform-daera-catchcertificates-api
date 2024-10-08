// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.Data;

public interface ICatchCertificateCaseRepository
{
    Task<Tuple<IEnumerable<CatchCertificateCaseDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize);

    Task<CatchCertificateCaseDataRow> GetByDocumentNumberAsync(string documentNumber);

    Task<CatchCertificateCaseDataRow> CreateAsync(CatchCertificateCaseDataRow dataRow);

    Task<CatchCertificateCaseDataRow> UpdateAsync(CatchCertificateCaseDataRow dataRow);
}