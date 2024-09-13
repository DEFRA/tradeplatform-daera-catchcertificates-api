// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Text.Json;
using AutoFixture;
using ModelsMmo = Defra.Trade.CatchCertificates.Api.Models;
using V1DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using V2DtosMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers;

public static class MmoFixtures
{
    #region "V1 MMO Fixtures"

    public static (ModelsMmo.CatchCertificateCaseDataRow, V1DtosMmo.CatchCertificateCase) CreateCatchCertificateCaseV1DataRow(
        Fixture fixture,
        string documentNumber,
        Action<V1DtosMmo.CatchCertificateCase> modificationDto = null,
        Action<ModelsMmo.CatchCertificateCaseDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V1DtosMmo.CatchCertificateCase>()
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.CatchCertificateCaseDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 1)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    public static (ModelsMmo.ProcessingStatementDataRow, V1DtosMmo.ProcessingStatement) CreateProcessingStatementV1DataRow(
        Fixture fixture,
        string documentNumber,
        Action<V1DtosMmo.ProcessingStatement> modificationDto = null,
        Action<ModelsMmo.ProcessingStatementDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V1DtosMmo.ProcessingStatement>()
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.ProcessingStatementDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 1)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    public static (ModelsMmo.StorageDocumentDataRow, V1DtosMmo.StorageDocument) CreateStorageDocumentV1DataRow(
        Fixture fixture,
        string documentNumber,
        Action<V1DtosMmo.StorageDocument> modificationDto = null,
        Action<ModelsMmo.StorageDocumentDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V1DtosMmo.StorageDocument>()
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.StorageDocumentDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 1)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    #endregion "V1 MMO Fixtures"

    #region "V2 MMO Fixtures"

    public static (ModelsMmo.CatchCertificateCaseDataRow, V2DtosMmo.CatchCertificateCase) CreateCatchCertificateCaseV2DataRow(
        Fixture fixture,
        int version,
        string documentNumber,
        Action<V2DtosMmo.CatchCertificateCase> modificationDto = null,
        Action<ModelsMmo.CatchCertificateCaseDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V2DtosMmo.CatchCertificateCase>()
            .With(c => c.Version, version)
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.CatchCertificateCaseDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 2)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    public static (ModelsMmo.ProcessingStatementDataRow, V2DtosMmo.ProcessingStatement) CreateProcessingStatementV2DataRow(
        Fixture fixture,
        int version,
        string documentNumber,
        Action<V2DtosMmo.ProcessingStatement> modificationDto = null,
        Action<ModelsMmo.ProcessingStatementDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V2DtosMmo.ProcessingStatement>()
            .With(c => c.Version, version)
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.ProcessingStatementDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 1)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    public static (ModelsMmo.StorageDocumentDataRow, V2DtosMmo.StorageDocument) CreateStorageDocumentV2DataRow(
        Fixture fixture,
        int version,
        string documentNumber,
        Action<V2DtosMmo.StorageDocument> modificationDto = null,
        Action<ModelsMmo.StorageDocumentDataRow> modificationDataRow = null)
    {
        var data = fixture
            .Build<V2DtosMmo.StorageDocument>()
            .With(c => c.Version, version)
            .With(c => c.DocumentNumber, documentNumber)
            .With(c => c.LastUpdatedSystem, "Test")
            .Create();

        modificationDto?.Invoke(data);

        var result = fixture
            .Build<ModelsMmo.StorageDocumentDataRow>()
            .With(r => r.DocumentNumber, documentNumber)
            .With(r => r.SchemaVersion, 1)
            .With(r => r.Data, JsonSerializer.Serialize(data))
            .Create();

        modificationDataRow?.Invoke(result);

        return (result, data);
    }

    #endregion "V2 MMO Fixtures"
}