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

public class StorageDocumentSqlRepository(IConnectionFactory connectionFactory) : RepositoryBase(connectionFactory), IStorageDocumentRepository
{
    public async Task<Tuple<IEnumerable<StorageDocumentDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize)
    {
        return await GetPaginatedAsync<StorageDocumentDataRow>(StorageDocumentSqlQueries.GetPaginated, pageNumber, pageSize);
    }

    public async Task<StorageDocumentDataRow> GetByDocumentNumberAsync(string documentNumber)
    {
        await using var connection = await OpenConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<StorageDocumentDataRow>(StorageDocumentSqlQueries.GetByDocumentNumber, new { documentNumber });
    }

    public async Task<StorageDocumentDataRow> CreateAsync(StorageDocumentDataRow dataRow)
    {
        await using var connection = await OpenConnectionAsync();

        dataRow.Id = await connection.ExecuteScalarAsync<int>(StorageDocumentSqlQueries.Create, dataRow);

        return dataRow;
    }

    public async Task<StorageDocumentDataRow> UpdateAsync(StorageDocumentDataRow dataRow)
    {
        await ExecuteAsync(StorageDocumentSqlQueries.Update, dataRow);

        return dataRow;
    }
}