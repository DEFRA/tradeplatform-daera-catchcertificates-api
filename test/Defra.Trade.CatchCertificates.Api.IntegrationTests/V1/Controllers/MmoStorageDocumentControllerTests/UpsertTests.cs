// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.Common.Api.Dtos;
using Moq;
using Xunit;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.CommonProblemDetailsAssertions;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.MmoFixtures;
using ModelsMmo = Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V1.Controllers.MmoStorageDocumentControllerTests
{
    public class UpsertTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory;
        private readonly Fixture fixture;

        public UpsertTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
            this.webApplicationFactory.ApiVersion = "1-internal";

            fixture = new Fixture();
        }

        [Fact]
        public async Task Upsert_UnknownApplication_CreatesNew()
        {
            var client = webApplicationFactory.CreateClient();

            var (_, dtoToSave) = CreateStorageDocumentV1DataRow(fixture, "GBR-2021-SD-012ABCDEF");

            var upsertResponse = await client.PostAsJsonAsync($"mmo-storage-document", dtoToSave);

            Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

            webApplicationFactory.StorageDocumentRepository
                .Verify(r =>
                    r.CreateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == dtoToSave.DocumentNumber)),
                    Times.Once);
        }

        [Fact]
        public async Task Upsert_KnownApplication_UpdatesExisting()
        {
            var client = webApplicationFactory.CreateClient();

            var (existingDataRow, existingDto) = CreateStorageDocumentV1DataRow(fixture, "GBR-2021-SD-777ABCDEF", null, dr => dr.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-1));

            webApplicationFactory.StorageDocumentRepository
                .Setup(r => r.GetByDocumentNumberAsync(existingDataRow.DocumentNumber))
                .ReturnsAsync(() => existingDataRow);

            existingDto.LastUpdated = DateTimeOffset.UtcNow;

            var upsertResponse = await client.PostAsJsonAsync($"mmo-storage-document", existingDto);

            Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

            webApplicationFactory.StorageDocumentRepository
                .Verify(r =>
                    r.UpdateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == existingDto.DocumentNumber)),
                    Times.Once);
        }

        [Fact]
        public async Task Upsert_KnownApplicationButOldMessage_DoesNotUpdate()
        {
            var client = webApplicationFactory.CreateClient();

            var (existingDataRow, existingDto) = CreateStorageDocumentV1DataRow(fixture, "GBR-2021-SD-111ABCDEF", null, dr => dr.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-1));

            webApplicationFactory.StorageDocumentRepository
                .Setup(r => r.GetByDocumentNumberAsync(existingDataRow.DocumentNumber))
                .ReturnsAsync(() => existingDataRow);

            existingDto.LastUpdated = DateTimeOffset.UtcNow.AddMinutes(-2);

            var upsertResponse = await client.PostAsJsonAsync($"mmo-storage-document", existingDto);

            Assert.Equal(HttpStatusCode.NoContent, upsertResponse.StatusCode);

            webApplicationFactory.StorageDocumentRepository
                .Verify(r =>
                    r.UpdateAsync(It.Is<ModelsMmo.StorageDocumentDataRow>(dr => dr.DocumentNumber == existingDto.DocumentNumber)),
                    Times.Never);
        }

        [Fact]
        public async Task Upsert_InvalidPayload_BadRequest()
        {
            var client = webApplicationFactory.CreateClient();

            var (_, dtoToSave) = CreateStorageDocumentV1DataRow(fixture, null, d => { d.LastUpdatedSystem = "12345678901"; d.LastUpdatedBy = new string('a', 101); });

            var response = await client.PostAsJsonAsync("mmo-storage-document", dtoToSave);
            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Verify(content, HttpStatusCode.BadRequest);
            Assert.NotNull(content.Errors["documentNumber"].FirstOrDefault());
            Assert.NotNull(content.Errors["lastUpdatedSystem"].FirstOrDefault());
            Assert.NotNull(content.Errors["lastUpdatedBy"].FirstOrDefault());
        }
    }
}
