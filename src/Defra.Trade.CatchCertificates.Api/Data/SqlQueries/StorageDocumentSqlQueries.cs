// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.Data.SqlQueries
{
    internal static class StorageDocumentSqlQueries
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
            FROM [mmo].[StorageDocument]
            WHERE [IsActive]=1
            ORDER BY [Id] ASC
            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;

            SELECT CAST(COUNT(1) as BIGINT)
            FROM [mmo].[StorageDocument]
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
            FROM [mmo].[StorageDocument]
            WHERE [DocumentNumber]=@documentNumber
            AND [IsActive]=1;";

        internal const string Create =
            @"INSERT INTO [mmo].[StorageDocument] (
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
            @"UPDATE [mmo].[StorageDocument]
            SET [Data]=@data, 
                [SchemaVersion]=@schemaVersion,
                [LastUpdated]=@lastUpdated,
                [LastUpdatedBy]=@lastUpdatedBy,
                [LastUpdatedSystem]=@lastUpdatedSystem
            WHERE [Id]=@id;";
    }
}
