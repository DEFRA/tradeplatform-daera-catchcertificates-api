// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Mappers;
using Defra.Trade.CatchCertificates.Api.Models;
using FluentAssertions;
using Xunit;
using V1Dto = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using V2Dto = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.Tests.Mappers;

public class MmoProfileTests
{
    private readonly IMapper _sut;

    public MmoProfileTests()
    {
        _sut = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MmoProfile>();
        }).CreateMapper();
    }

    public static TheoryData<object, object> Map_Correctly_TheoryData()
    {
        return new()
        {
            { new CatchCertificateCaseDataRow(), new V1Dto.CatchCertificateCaseEntry() },
            {
                new CatchCertificateCaseDataRow
                {
                    SchemaVersion = 1,
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                    CreatedOn = DateTimeOffset.MinValue,
                    CreatedBy = "FAIL",
                    CreatedSystem = "FAIL",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.CatchCertificateCase
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new CatchCertificateCaseDataRow
                {
                    SchemaVersion = 1,
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "abc123",
                    CreatedSystem = "def456",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.CatchCertificateCase
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new V1Dto.CatchCertificateCase
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new CatchCertificateCaseDataRow
                {
                    SchemaVersion = 1,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"Audits\":null,\"CertStatus\":null,\"IsDirectLanding\":null,\"IsUnblocked\":null,\"Landings\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
            {
                new V2Dto.CatchCertificateCase
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new CatchCertificateCaseDataRow
                {
                    SchemaVersion = 2,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"Audits\":null,\"CertStatus\":null,\"FailureIrrespectiveOfRisk\":null,\"IsDirectLanding\":null,\"IsUnblocked\":null,\"Landings\":null,\"MultiVesselSchedule\":null,\"SpeciesOverriddenByAdmin\":null,\"Transportation\":null,\"Version\":null,\"VesselOverriddenByAdmin\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
            { new ProcessingStatementDataRow(), new V1Dto.ProcessingStatementEntry() },
            {
                new ProcessingStatementDataRow
                {
                    SchemaVersion = 1,
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                    CreatedOn = DateTimeOffset.MinValue,
                    CreatedBy = "FAIL",
                    CreatedSystem = "FAIL",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.ProcessingStatement
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new ProcessingStatementDataRow
                {
                    SchemaVersion = 1,
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "abc123",
                    CreatedSystem = "def456",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.ProcessingStatement
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new V1Dto.ProcessingStatement
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new ProcessingStatementDataRow
                {
                    SchemaVersion = 1,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"Catches\":null,\"ExporterId\":null,\"PersonResponsible\":null,\"PlantName\":null,\"ProcessedFisheryProducts\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
            {
                new V2Dto.ProcessingStatement
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new ProcessingStatementDataRow
                {
                    SchemaVersion = 2,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"Authority\":null,\"Catches\":null,\"ExporterId\":null,\"HealthCertificateDate\":null,\"HealthCertificateNumber\":null,\"PersonResponsible\":null,\"PlantAddress\":null,\"PlantApprovalNumber\":null,\"PlantDateOfAcceptance\":null,\"PlantName\":null,\"ProcessedFisheryProducts\":null,\"Version\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
            { new StorageDocumentDataRow(), new V1Dto.StorageDocumentEntry() },
            {
                new StorageDocumentDataRow
                {
                    SchemaVersion = 1,
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                    CreatedOn = DateTimeOffset.MinValue,
                    CreatedBy = "FAIL",
                    CreatedSystem = "FAIL",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.StorageDocument
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new StorageDocumentDataRow
                {
                    SchemaVersion = 1,
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "abc123",
                    CreatedSystem = "def456",
                    Data = "{\"DocumentNumber\":\"abc123\",\"LastUpdated\":\"0001-01-01T00:00:00Z\",\"LastUpdatedBy\":\"Incorrect\",\"LastUpdatedSystem\":\"Incorrect\"}"
                },
                new V1Dto.StorageDocument
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "abc123",
                    LastUpdatedSystem = "def456",
                }
            },
            {
                new V1Dto.StorageDocument
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new StorageDocumentDataRow
                {
                    SchemaVersion = 1,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"CompanyName\":null,\"ExporterId\":null,\"Products\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
            {
                new V2Dto.StorageDocument
                {
                    DocumentNumber = "abc123",
                    LastUpdated = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    LastUpdatedBy = "def456",
                    LastUpdatedSystem = "ghi789"
                },
                new StorageDocumentDataRow
                {
                    SchemaVersion = 2,
                    DocumentNumber = "abc123",
                    CreatedOn = new(2023, 11, 17, 14, 33, 00, TimeSpan.Zero),
                    CreatedBy = "def456",
                    CreatedSystem = "ghi789",
                    Data = "{\"Authority\":null,\"CompanyName\":null,\"ExporterId\":null,\"Products\":null,\"StorageFacilities\":null,\"Transportation\":null,\"Version\":null,\"CaseType1\":null,\"CaseType2\":null,\"_correlationId\":null,\"DA\":null,\"DocumentDate\":null,\"DocumentNumber\":\"abc123\",\"DocumentUrl\":null,\"ExportedTo\":null,\"Exporter\":null,\"LastUpdated\":\"2023-11-17T14:33:00+00:00\",\"LastUpdatedBy\":\"def456\",\"LastUpdatedSystem\":\"ghi789\",\"NumberOfFailedSubmissions\":null,\"RequestedByAdmin\":null}"
                }
            },
        };
    }

    [Theory]
    [MemberData(nameof(Map_Correctly_TheoryData))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable", Justification = "Data enumerates")]
    public void Map_Correctly<TSource, TDestination>(TSource source, TDestination expected)
    {
        // arrange

        // act
        var actual = _sut.Map<TDestination>(source);

        // assert
        actual.Should().BeEquivalentTo(expected);
    }
}