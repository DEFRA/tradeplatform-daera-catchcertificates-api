// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.Data.SqlQueries
{
    internal static class ProcessingStatementSqlQueries
    {
        internal const string GetPaginated =
            @"SELECT [Id],
                [DocumentNumber],
                [SchemaVersion],
                [CreatedOn],
	            [CreatedBy],
	            [CreatedSystem],
	            [LastUpdated],
	            [LastUpdatedBy],
	            [LastUpdatedSystem]
            FROM [mmo].[ProcessingStatement]
            WHERE [IsActive]=1
            ORDER BY [Id] ASC
            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;

            SELECT CAST(COUNT(1) as BIGINT)
            FROM [mmo].[ProcessingStatement]
            WHERE [IsActive]=1;";

        internal const string GetByDocumentNumber =
            @"SELECT [Id],
                [DocumentNumber],
                [Data],
                [SchemaVersion],
                [CreatedOn],
	            [CreatedBy],
	            [CreatedSystem],
	            [LastUpdated],
	            [LastUpdatedBy],
	            [LastUpdatedSystem],
	            [IsActive]
            FROM [mmo].[ProcessingStatement]
            WHERE [DocumentNumber]=@documentNumber
            AND [IsActive]=1;";

        internal const string Create =
            @"INSERT INTO [mmo].[ProcessingStatement] (
                [DocumentNumber],
                [Data],
                [SchemaVersion],
                [CreatedOn],
	            [CreatedBy],
	            [CreatedSystem],
	            [IsActive])
            OUTPUT inserted.Id
            VALUES (
                @documentNumber,
                @data, 
                @schemaVersion,
                @createdOn,
                @createdBy,
                @createdSystem,
                1);";

        internal const string Update =
            @"UPDATE [mmo].[ProcessingStatement]
            SET [Data]=@data, 
                [SchemaVersion]=@schemaVersion,
                [LastUpdated]=@lastUpdated,
                [LastUpdatedBy]=@lastUpdatedBy,
                [LastUpdatedSystem]=@lastUpdatedSystem
            WHERE [Id]=@id;";
    }
}
