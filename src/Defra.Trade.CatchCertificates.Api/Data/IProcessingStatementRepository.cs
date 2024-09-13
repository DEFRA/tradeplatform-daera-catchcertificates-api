// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.Data
{
    public interface IProcessingStatementRepository
    {
        Task<Tuple<IEnumerable<ProcessingStatementDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize);

        Task<ProcessingStatementDataRow> GetByDocumentNumberAsync(string documentNumber);

        Task<ProcessingStatementDataRow> CreateAsync(ProcessingStatementDataRow dataRow);

        Task<ProcessingStatementDataRow> UpdateAsync(ProcessingStatementDataRow dataRow);
    }
}