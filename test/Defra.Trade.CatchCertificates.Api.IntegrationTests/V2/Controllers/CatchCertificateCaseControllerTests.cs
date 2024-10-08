// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.Common.Api.Dtos;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.CommonProblemDetailsAssertions;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.MmoFixtures;
using AuditingModels = Defra.Trade.Common.ExternalApi.Auditing.Models;
using DtosInboundMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using DtosOutboundMmo = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V2.Controllers;

public class CatchCertificateCaseControllerTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
{
    private const string IdExistingItem = "GBR-2021-CC-444ABCDEF";
    private const string IdNotFound = "123";
    private readonly Guid _defaultClientId;
    private readonly string _defaultClientIPAddress;
    private readonly Models.CatchCertificateCaseDataRow _existingDataRow;
    private readonly DtosInboundMmo.CatchCertificateCase _existingDto;
    private readonly Fixture _fixture;
    private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;

    public CatchCertificateCaseControllerTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _webApplicationFactory.ApiVersion = "2";

        _fixture = new Fixture();

        (_existingDataRow, _existingDto) = CreateCatchCertificateCaseV2DataRow(_fixture, 2, IdExistingItem);

        _webApplicationFactory.CatchCertificateRepository
            .Setup(r => r.GetByDocumentNumberAsync(_existingDataRow.DocumentNumber))
            .ReturnsAsync(() => _existingDataRow);

        _defaultClientId = Guid.NewGuid();
        _defaultClientIPAddress = "12.34.56.78";
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

        _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV2CatchCertificateCaseGetById,
            _defaultClientId, IdNotFound, HttpMethods.Get,
            $"/catch-certificate-case/{IdNotFound}", null, StatusCodes.Status404NotFound, sentAt, false, false, _defaultClientIPAddress);
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
        content.Should().BeEquivalentTo(_existingDto, opt => opt
            .Excluding(c => c.LastUpdated)
            .Excluding(c => c.LastUpdatedBy)
            .Excluding(c => c.LastUpdatedSystem)
            .Excluding((IMemberInfo m) => m.DeclaringType == typeof(DtosInboundMmo.DynamicsAddress) && (
                m.Name == nameof(DtosInboundMmo.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty)
                || m.Name == nameof(DtosInboundMmo.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname)
                || m.Name == nameof(DtosInboundMmo.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue)
                || m.Name == nameof(DtosInboundMmo.DynamicsAddress.DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue)
            )));

        _webApplicationFactory.AuditRepository.VerifyAuditLogged(AuditingModels.Enums.AuditLogType.DaeraFishExportServiceV2CatchCertificateCaseGetById,
            _defaultClientId, IdExistingItem, HttpMethods.Get,
            $"/catch-certificate-case/{IdExistingItem}", null, StatusCodes.Status200OK, sentAt, false, true, _defaultClientIPAddress);
    }
}