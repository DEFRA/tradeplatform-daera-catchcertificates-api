// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.Data.SqlQueries;

internal static class CatchCertificateCaseSqlQueries
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
            FROM [mmo].[CatchCertificateCase]
            WHERE [IsActive]=1
            ORDER BY [Id] ASC
            OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;

            SELECT CAST(COUNT(1) as BIGINT)
            FROM [mmo].[CatchCertificateCase]
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
            FROM [mmo].[CatchCertificateCase]
            WHERE [DocumentNumber]=@documentNumber
            AND [IsActive]=1;";

    internal const string Create =
        @"INSERT INTO [mmo].[CatchCertificateCase] (
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
        @"UPDATE [mmo].[CatchCertificateCase]
            SET [Data]=@data, 
                [SchemaVersion]=@schemaVersion,
                [LastUpdated]=@lastUpdated,
                [LastUpdatedBy]=@lastUpdatedBy,
                [LastUpdatedSystem]=@lastUpdatedSystem
            WHERE [Id]=@id;";
}
