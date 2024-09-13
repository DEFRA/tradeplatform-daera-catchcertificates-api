// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.Common.Api.Dtos;
using Moq;
using Xunit;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.CommonProblemDetailsAssertions;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.MmoFixtures;
using ModelsMmo = Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V2.Controllers.MmoStorageDocumentControllerTests;

public class UpsertTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
{
    private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
    private readonly Fixture _fixture;
    private const int Version = 2;

    public UpsertTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _webApplicationFactory.ApiVersion = "2-internal";

        _fixture = new Fixture();
    }

    [Fact]
    public async Task Upsert_UnknownApplication_CreatesNew()
    {
        var client = _webApplicationFactory.CreateClient();

        var (_, dtoToSave) = CreateStorageDocumentV2DataRow(_fixture, Version, "GBR-2023-CC-223UVWXYZ");

        using var content = new StringContent(JsonSerializer.Serialize(dtoToSave), null, "application/json");
        using var upsertResponse = await client.PostAsync($"mmo-storage-document", content);

        Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

        _webApplicationFactory.StorageDocumentRepository
            .Verify(r =>
                    r.CreateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == dtoToSave.DocumentNumber)),
                Times.Once);
    }

    [Fact]
    public async Task Upsert_KnownApplication_UpdatesExisting()
    {
        var client = _webApplicationFactory.CreateClient();

        var (existingDataRow, existingDto) = CreateStorageDocumentV2DataRow(_fixture, Version, "GBR-2023-CC-323UVWXYZ", null, dr => dr.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-1));

        _webApplicationFactory.StorageDocumentRepository
            .Setup(r => r.GetByDocumentNumberAsync(existingDataRow.DocumentNumber))
            .ReturnsAsync(() => existingDataRow);

        existingDto.LastUpdated = DateTimeOffset.UtcNow;

        using var content = new StringContent(JsonSerializer.Serialize(existingDto), null, "application/json");
        using var upsertResponse = await client.PostAsync($"mmo-storage-document", content);

        Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

        _webApplicationFactory.StorageDocumentRepository
            .Verify(r =>
                    r.UpdateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == existingDto.DocumentNumber)),
                Times.Once);
    }

    [Fact]
    public async Task Upsert_KnownApplicationButOldMessage_DoesNotUpdate()
    {
        var client = _webApplicationFactory.CreateClient();

        var (existingDataRow, existingDto) = CreateStorageDocumentV2DataRow(_fixture, Version, "GBR-2021-CC-500ABCDEF", null, dr => dr.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-1));

        _webApplicationFactory.StorageDocumentRepository
            .Setup(r => r.GetByDocumentNumberAsync(existingDataRow.DocumentNumber))
            .ReturnsAsync(() => existingDataRow);

        existingDto.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-2);

        using var content = new StringContent(JsonSerializer.Serialize(existingDto), null, "application/json");
        using var upsertResponse = await client.PostAsync($"mmo-storage-document", content);

        Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

        _webApplicationFactory.StorageDocumentRepository
            .Verify(r =>
                    r.UpdateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == existingDto.DocumentNumber)),
                Times.Never);
    }

    [Fact]
    public async Task Upsert_InvalidPayload_BadRequest()
    {
        int invalidVersion = 1;

        var client = _webApplicationFactory.CreateClient();

        var (_, dtoToSave) = CreateStorageDocumentV2DataRow(_fixture, invalidVersion, null, d => { d.LastUpdatedSystem = "12345678901"; d.LastUpdatedBy = new string('a', 101); });

        using var content = new StringContent(JsonSerializer.Serialize(dtoToSave), null, "application/json");
        using var response = await client.PostAsync("mmo-storage-document", content);
        var problem = await response.Content.ReadAsAsync<CommonProblemDetails>();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Verify(problem, HttpStatusCode.BadRequest);
        Assert.NotNull(problem.Errors["version"].FirstOrDefault());
        Assert.NotNull(problem.Errors["documentNumber"].FirstOrDefault());
        Assert.NotNull(problem.Errors["lastUpdatedSystem"].FirstOrDefault());
        Assert.NotNull(problem.Errors["lastUpdatedBy"].FirstOrDefault());
    }
}