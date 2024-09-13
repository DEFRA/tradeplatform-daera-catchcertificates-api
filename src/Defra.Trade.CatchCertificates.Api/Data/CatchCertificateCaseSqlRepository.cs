// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Defra.Trade.CatchCertificates.Api.Data.SqlQueries;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.Common.Sql.Data;
using Defra.Trade.Common.Sql.Infrastructure;

namespace Defra.Trade.CatchCertificates.Api.Data
{
    public class CatchCertificateCaseSqlRepository(IConnectionFactory connectionFactory) : RepositoryBase(connectionFactory), ICatchCertificateCaseRepository
    {
        public async Task<Tuple<IEnumerable<CatchCertificateCaseDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize)
        {
            return await GetPaginatedAsync<CatchCertificateCaseDataRow>(CatchCertificateCaseSqlQueries.GetPaginated, pageNumber, pageSize);
        }

        public async Task<CatchCertificateCaseDataRow> GetByDocumentNumberAsync(string documentNumber)
        {
            await using var connection = await OpenConnectionAsync();

            return await connection.QuerySingleOrDefaultAsync<CatchCertificateCaseDataRow>(CatchCertificateCaseSqlQueries.GetByDocumentNumber, new { documentNumber });
        }

        public async Task<CatchCertificateCaseDataRow> CreateAsync(CatchCertificateCaseDataRow dataRow)
        {
            await using var connection = await OpenConnectionAsync();

            dataRow.Id = await connection.ExecuteScalarAsync<int>(CatchCertificateCaseSqlQueries.Create, dataRow);

            return dataRow;
        }

        public async Task<CatchCertificateCaseDataRow> UpdateAsync(CatchCertificateCaseDataRow dataRow)
        {
            await ExecuteAsync(CatchCertificateCaseSqlQueries.Update, dataRow);

            return dataRow;
        }
    }
}