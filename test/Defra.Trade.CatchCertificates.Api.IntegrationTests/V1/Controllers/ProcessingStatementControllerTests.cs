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
    public class ProcessingStatementControllerTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
        private readonly Fixture _fixture;
        private const string IdExistingItem = "GBR-2021-PS-111ABCDEF";
        private const string IdNotFound = "ABC";

        private readonly Models.ProcessingStatementDataRow _existingDataRow;
        private readonly DtosInboundMmo.ProcessingStatement _existingDto;

        private readonly Guid _defaultClientId;
        private readonly string _defaultClientIPAddress;

        public ProcessingStatementControllerTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _webApplicationFactory.ApiVersion = "1";

            _defaultClientId = Guid.NewGuid();
            _defaultClientIPAddress = "12.34.56.78";

            _fixture = new Fixture();

            (_existingDataRow, _existingDto) = CreateProcessingStatementV1DataRow(_fixture, IdExistingItem);

            _webApplicationFactory.ProcessingStatementRepository
                .Setup(r => r.GetByDocumentNumberAsync(_existingDataRow.DocumentNumber))
                .ReturnsAsync(() => _existingDataRow);
        }

        [Fact]
        public async Task GetById_ValidId_Success()
        {
            var client = _webApplicationFactory.CreateClient();
            var sentAt = DateTimeOffset.UtcNow;

            _webApplicationFactory.AddApimUserContextHeaders(client, _defaultClientId, _defaultClientIPAddress);

            var response = await client.GetAsync($"processing-statement/{IdExistingItem}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DtosOutboundMmo.ProcessingStatement>();

            content.Should().NotBeNull();

            content.DocumentNumber.Should().Be(IdExistingItem);
            content.DA.Should().Be(_existingDto.DA);
            content.DocumentUrl.Should().Be(_existingDto.DocumentUrl);
            content.DocumentDate.Should().Be(_existingDto.DocumentDate);
            content.PersonResponsible.Should().Be(_existingDto.PersonResponsible);
            content.PlantName.Should().Be(_existingDto.PlantName);
            content.ProcessedFisheryProducts.Should().Be(_existingDto.ProcessedFisheryProducts);

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

            content.Catches.Should().HaveCount(_existingDto.Catches.Count());

            for (int i = 0; i < _existingDto.Catches.Count(); i++)
            {
                var actualLanding = content.Catches.ToList()[i];
                var expectedLanding = content.Catches.ToList()[i];

                expectedLanding.CnCode.Should().Be(actualLanding.CnCode);
                expectedLanding.ForeignCatchCertificateNumber.Should().Be(actualLanding.ForeignCatchCertificateNumber);
                expectedLanding.Id.Should().Be(actualLanding.Id);
                expectedLanding.ImportedWeight.Should().Be(actualLanding.ImportedWeight);
                expectedLanding.ProcessedWeight.Should().Be(actualLanding.ProcessedWeight);
                expectedLanding.Species.Should().Be(actualLanding.Species);
                expectedLanding.ScientificName.Should().Be(actualLanding.ScientificName);
                expectedLanding.UsedWeightAgainstCertificate.Should().Be(actualLanding.UsedWeightAgainstCertificate);
            }

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1ProcessingStatementGetById,
                _defaultClientId, IdExistingItem, HttpMethods.Get,
                $"/processing-statement/{IdExistingItem}", null, StatusCodes.Status200OK, sentAt, false, true, _defaultClientIPAddress);
        }

        [Fact]
        public async Task GetById_InvalidId_NotFound()
        {
            var client = _webApplicationFactory.CreateClient();
            var sentAt = DateTimeOffset.UtcNow;

            _webApplicationFactory.AddApimUserContextHeaders(client, _defaultClientId, _defaultClientIPAddress);

            var response = await client.GetAsync($"processing-statement/{IdNotFound}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.NotFound);

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1ProcessingStatementGetById,
                _defaultClientId, IdNotFound, HttpMethods.Get,
                $"/processing-statement/{IdNotFound}", null, StatusCodes.Status404NotFound, sentAt, false, false, _defaultClientIPAddress);
        }
    }
}