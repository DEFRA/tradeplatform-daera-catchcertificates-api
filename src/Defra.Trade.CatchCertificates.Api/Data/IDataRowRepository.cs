// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.Data;

public interface IDataRowRepository<TDataRow> where TDataRow : DataRow
{
    Task<TDataRow> CreateAsync(TDataRow dataRow);

    Task<TDataRow> GetByDocumentNumberAsync(string documentNumber);

    Task<Tuple<IEnumerable<TDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize);

    Task<TDataRow> UpdateAsync(TDataRow dataRow);
}
