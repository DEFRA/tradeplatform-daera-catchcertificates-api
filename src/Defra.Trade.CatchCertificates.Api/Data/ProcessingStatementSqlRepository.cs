// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Defra.Trade.CatchCertificates.Api.Data.SqlQueries;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.Common.Sql.Data;
using Defra.Trade.Common.Sql.Infrastructure;

namespace Defra.Trade.CatchCertificates.Api.Data;

public class ProcessingStatementSqlRepository(IConnectionFactory connectionFactory) : RepositoryBase(connectionFactory), IProcessingStatementRepository
{
    public async Task<Tuple<IEnumerable<ProcessingStatementDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize)
    {
        return await GetPaginatedAsync<ProcessingStatementDataRow>(ProcessingStatementSqlQueries.GetPaginated, pageNumber, pageSize);
    }

    public async Task<ProcessingStatementDataRow> GetByDocumentNumberAsync(string documentNumber)
    {
        await using var connection = await OpenConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<ProcessingStatementDataRow>(ProcessingStatementSqlQueries.GetByDocumentNumber, new { documentNumber });
    }

    public async Task<ProcessingStatementDataRow> CreateAsync(ProcessingStatementDataRow dataRow)
    {
        await using var connection = await OpenConnectionAsync();

        dataRow.Id = await connection.ExecuteScalarAsync<int>(ProcessingStatementSqlQueries.Create, dataRow);

        return dataRow;
    }

    public async Task<ProcessingStatementDataRow> UpdateAsync(ProcessingStatementDataRow dataRow)
    {
        await ExecuteAsync(ProcessingStatementSqlQueries.Update, dataRow);

        return dataRow;
    }
}