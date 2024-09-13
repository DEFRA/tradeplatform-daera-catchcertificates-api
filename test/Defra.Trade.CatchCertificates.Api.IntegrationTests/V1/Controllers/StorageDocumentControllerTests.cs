// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.Common.Api.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.CommonProblemDetailsAssertions;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.MmoFixtures;
using AuditingModels = Defra.Trade.Common.ExternalApi.Auditing.Models;
using DtosInboundMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutboundMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V1.Controllers
{
    public class StorageDocumentControllerTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
        private readonly Fixture _fixture;
        private const string IdExistingItem = "GBR-2021-SD-888ABCDEF";
        private const string IdNotFound = "ABC";

        private readonly Models.StorageDocumentDataRow _existingDataRow;
        private readonly DtosInboundMmo.StorageDocument _existingDto;

        private readonly Guid _defaultClientId;
        private readonly string _defaultClientIPAddress;

        public StorageDocumentControllerTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _webApplicationFactory.ApiVersion = "1";

            _fixture = new Fixture();

            (_existingDataRow, _existingDto) = CreateStorageDocumentV1DataRow(_fixture, IdExistingItem);

            _webApplicationFactory.StorageDocumentRepository
                .Setup(r => r.GetByDocumentNumberAsync(_existingDataRow.DocumentNumber))
                .ReturnsAsync(() => _existingDataRow);

            _defaultClientId = Guid.NewGuid();
            _defaultClientIPAddress = "12.34.56.78";
        }

        [Fact]
        public async Task GetById_ValidId_Success()
        {
            var client = _webApplicationFactory.CreateClient();
            var sentAt = DateTimeOffset.UtcNow;

            _webApplicationFactory.AddApimUserContextHeaders(client, _defaultClientId, _defaultClientIPAddress);

            var response = await client.GetAsync($"storage-document/{IdExistingItem}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DtosOutboundMmo.StorageDocument>();

            content.Should().NotBeNull();

            content.DocumentNumber.Should().Be(IdExistingItem);
            content.DA.Should().Be(_existingDto.DA);
            content.CompanyName.Should().Be(_existingDto.CompanyName);
            content.DocumentUrl.Should().Be(_existingDto.DocumentUrl);
            content.DocumentDate.Should().Be(_existingDto.DocumentDate);

            content.Exporter.Address.City.Should().Be(_existingDto.Exporter.Address.City);
            content.Exporter.Address.Line1.Should().Be(_existingDto.Exporter.Address.Line1);
            content.Exporter.Address.Line2.Should().Be(_existingDto.Exporter.Address.Line2);
            content.Exporter.Address.PostCode.Should().Be(_existingDto.Exporter.Address.PostCode);
            content.Exporter.Address.County.Should().Be(_existingDto.Exporter.Address.County);
            content.Exporter.Address.Country.Should().Be(_existingDto.Exporter.Address.Country);
            content.Exporter.CompanyName.Should().Be(_existingDto.Exporter.CompanyName);
            content.Exporter.FullName.Should().Be(_existingDto.Exporter.FullName);

            content.ExportedTo.OfficialCountryName.Should().Be(_existingDto.ExportedTo.OfficialCountryName);
            content.ExportedTo.IsoCodeAlpha2.Should().Be(_existingDto.ExportedTo.IsoCodeAlpha2);
            content.ExportedTo.IsoCodeAlpha3.Should().Be(_existingDto.ExportedTo.IsoCodeAlpha3);
            content.ExportedTo.IsoNumericCode.Should().Be(_existingDto.ExportedTo.IsoNumericCode);

            content.Products.Should().HaveCount(_existingDto.Products.Count());

            for (int i = 0; i < _existingDto.Products.Count(); i++)
            {
                var actualDoc = content.Products.ToList()[i];
                var expectedDoc = content.Products.ToList()[i];

                expectedDoc.CnCode.Should().Be(actualDoc.CnCode);
                expectedDoc.Id.Should().Be(actualDoc.Id);
                expectedDoc.ExportedWeight.Should().Be(actualDoc.ExportedWeight);
                expectedDoc.ForeignCatchCertificateNumber.Should().Be(actualDoc.ForeignCatchCertificateNumber);
                expectedDoc.ImportedWeight.Should().Be(actualDoc.ImportedWeight);
                expectedDoc.Species.Should().Be(actualDoc.Species);
                expectedDoc.ScientificName.Should().Be(actualDoc.ScientificName);
            }

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1StorageDocumentGetById,
                _defaultClientId, IdExistingItem, HttpMethods.Get,
                $"/storage-document/{IdExistingItem}", null, StatusCodes.Status200OK, sentAt, false, true, _defaultClientIPAddress);
        }

        [Fact]
        public async Task GetById_InvalidId_NotFound()
        {
            var client = _webApplicationFactory.CreateClient();
            var sentAt = DateTimeOffset.UtcNow;

            _webApplicationFactory.AddApimUserContextHeaders(client, _defaultClientId, _defaultClientIPAddress);

            var response = await client.GetAsync($"storage-document/{IdNotFound}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.NotFound);

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1StorageDocumentGetById,
                _defaultClientId, IdNotFound, HttpMethods.Get,
                $"/storage-document/{IdNotFound}", null, StatusCodes.Status404NotFound, sentAt, false, false, _defaultClientIPAddress);
        }
    }
}