// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Text.Json;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.Mappers;

public class MmoProfile : Profile
{
    public MmoProfile()
    {
        // Catch Certificate Cases V2
        CreateDataRowToModelMapping<CatchCertificateCaseDataRow, V2.Dtos.Mmo.CatchCertificateCase>(schemaRange: 1..2);
        CreateModelToDataRowMapping<V2.Dtos.Mmo.CatchCertificateCase, CatchCertificateCaseDataRow>(schemaVersion: 2);

        // Processing Statements V2
        CreateDataRowToModelMapping<ProcessingStatementDataRow, V2.Dtos.Mmo.ProcessingStatement>(schemaRange: 1..2);
        CreateModelToDataRowMapping<V2.Dtos.Mmo.ProcessingStatement, ProcessingStatementDataRow>(schemaVersion: 2);

        // Storage Documents V2
        CreateDataRowToModelMapping<StorageDocumentDataRow, V2.Dtos.Mmo.StorageDocument>(schemaRange: 1..2);
        CreateModelToDataRowMapping<V2.Dtos.Mmo.StorageDocument, StorageDocumentDataRow>(schemaVersion: 2);
    }

    private void CreateDataRowToModelMapping<TDataRow, TModel>(Range schemaRange)
        where TDataRow : DataRow
        where TModel : IUpdateTracked
    {
        int minVersion = schemaRange.Start.GetOffset(0);
        int maxVersion = schemaRange.End.GetOffset(0);
        CreateMap<TDataRow, TModel>()
            .ConvertUsing((source, d) =>
            {
                if (source.SchemaVersion < minVersion || source.SchemaVersion > maxVersion)
                {
                    throw new ArgumentException($"Schema version was expected to be in {schemaRange} but was actually {source.SchemaVersion}", nameof(source));
                }

                var result = JsonSerializer.Deserialize<TModel>(source.Data);
                result.LastUpdated = source.LastUpdated ?? source.CreatedOn;
                result.LastUpdatedBy = source.LastUpdatedBy ?? source.CreatedBy;
                result.LastUpdatedSystem = source.LastUpdatedSystem ?? source.CreatedSystem;

                return result;
            });
    }

    private void CreateModelToDataRowMapping<TModel, TDataRow>(int schemaVersion) where TDataRow : DataRow
    {
        CreateMap<TModel, TDataRow>()
            .ForMember(d => d.SchemaVersion, opt => opt.MapFrom(s => schemaVersion))
            .ForMember(d => d.Data, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s)))
            .AfterMap((s, d) =>
            {
                // If this is a create
                if (d.CreatedOn == default && d.CreatedBy is null && d.CreatedSystem is null)
                {
                    d.CreatedBy = d.LastUpdatedBy;
                    d.CreatedOn = d.LastUpdated.GetValueOrDefault();
                    d.CreatedSystem = d.LastUpdatedSystem;
                    d.LastUpdatedBy = null;
                    d.LastUpdated = null;
                    d.LastUpdatedSystem = null;
                }
            });
    }
}
