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
    public class CatchCertificateCaseControllerTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
        private readonly Fixture _fixture;
        private const string IdExistingItem = "GBR-2021-CC-444ABCDEF";
        private const string IdNotFound = "123";
        private readonly Models.CatchCertificateCaseDataRow _existingDataRow;
        private readonly DtosInboundMmo.CatchCertificateCase _existingDto;

        private readonly Guid _defaultClientId;
        private readonly string _defaultClientIPAddress;

        public CatchCertificateCaseControllerTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _webApplicationFactory.ApiVersion = "1";

            _fixture = new Fixture();

            (_existingDataRow, _existingDto) = CreateCatchCertificateCaseV1DataRow(_fixture, IdExistingItem);

            _webApplicationFactory.CatchCertificateRepository
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

            var response = await client.GetAsync($"catch-certificate-case/{IdExistingItem}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DtosOutboundMmo.CatchCertificateCase>();

            content.Should().NotBeNull();

            content.DocumentNumber.Should().Be(IdExistingItem);
            content.CertStatus.Should().Be(_existingDto.CertStatus);
            content.DA.Should().Be(_existingDto.DA);
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

            content.Landings.Should().HaveCount(_existingDto.Landings.Count());

            for (int i = 0; i < _existingDto.Landings.Count(); i++)
            {
                var actualLanding = content.Landings.ToList()[i];
                var expectedLanding = content.Landings.ToList()[i];

                expectedLanding.Id.Should().Be(actualLanding.Id);
                expectedLanding.LandingDate.Should().Be(actualLanding.LandingDate);
                expectedLanding.NumberOfTotalSubmissions.Should().Be(actualLanding.NumberOfTotalSubmissions);
                expectedLanding.Presentation.Should().Be(actualLanding.Presentation);
                expectedLanding.Source.Should().Be(actualLanding.Source);
                expectedLanding.Species.Should().Be(actualLanding.Species);
                expectedLanding.ScientificName.Should().Be(actualLanding.ScientificName);
                expectedLanding.CnCode.Should().Be(actualLanding.CnCode);
                expectedLanding.CommodityCodeDescription.Should().Be(actualLanding.CommodityCodeDescription);
                expectedLanding.State.Should().Be(actualLanding.State);
                expectedLanding.Status.Should().Be(actualLanding.Status);
                expectedLanding.Validation.LandedWeightExceededBy.Should().Be(actualLanding.Validation.LandedWeightExceededBy);
                expectedLanding.Validation.LiveExportWeight.Should().Be(actualLanding.Validation.LiveExportWeight);
                expectedLanding.Validation.TotalEstimatedForExportSpecies.Should().Be(actualLanding.Validation.TotalEstimatedForExportSpecies);
                expectedLanding.Validation.TotalEstimatedWithTolerance.Should().Be(actualLanding.Validation.TotalEstimatedWithTolerance);
                expectedLanding.Validation.TotalLiveForExportSpecies.Should().Be(actualLanding.Validation.TotalLiveForExportSpecies);
                expectedLanding.Validation.TotalRecordedAgainstLanding.Should().Be(actualLanding.Validation.TotalRecordedAgainstLanding);
                expectedLanding.Validation.TotalWeightForSpecies.Should().Be(actualLanding.Validation.TotalWeightForSpecies);
                expectedLanding.VesselName.Should().Be(actualLanding.VesselName);
                expectedLanding.VesselPln.Should().Be(actualLanding.VesselPln);
                expectedLanding.VesselLength.Should().Be(actualLanding.VesselLength);
                expectedLanding.LicenceHolder.Should().Be(actualLanding.LicenceHolder);
                expectedLanding.Weight.Should().Be(actualLanding.Weight);
            }

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1CatchCertificateCaseGetById,
                _defaultClientId, IdExistingItem, HttpMethods.Get,
                $"/catch-certificate-case/{IdExistingItem}", null, StatusCodes.Status200OK, sentAt, false, true, _defaultClientIPAddress);
        }

        [Fact]
        public async Task GetById_InvalidId_NotFound()
        {
            var client = _webApplicationFactory.CreateClient();
            var sentAt = DateTimeOffset.UtcNow;

            _webApplicationFactory.AddApimUserContextHeaders(client, _defaultClientId, _defaultClientIPAddress);

            var response = await client.GetAsync($"catch-certificate-case/{IdNotFound}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.NotFound);

            _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV1CatchCertificateCaseGetById,
                _defaultClientId, IdNotFound, HttpMethods.Get,
                $"/catch-certificate-case/{IdNotFound}", null, StatusCodes.Status404NotFound, sentAt, false, false, _defaultClientIPAddress);
        }
    }
}