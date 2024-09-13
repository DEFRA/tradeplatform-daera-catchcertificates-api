// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.Common.Api.Dtos;
using FluentAssertions;
using Moq;
using Xunit;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.CommonProblemDetailsAssertions;
using static Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers.MmoFixtures;
using DtosEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using ModelsMmo = Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V1.Controllers
{
    public class EhcoMmoMappingControllerTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
        private readonly Fixture _fixture;
        private readonly ModelsMmo.CatchCertificateCaseDataRow _existingCatchCert;

        public EhcoMmoMappingControllerTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _webApplicationFactory.ApiVersion = "1";

            _fixture = new Fixture();

            (_existingCatchCert, _) = CreateCatchCertificateCaseV1DataRow(_fixture, "GBR-2021-CC-123ABCDEF", c =>
            {
                c.DA = "ENGLAND";
                c.Exporter.AccountId = "1C4E19B5-82BD-41F4-8994-6104C37DC9C2";
                c.Exporter.FullName = "Dave Fish";
                c.Exporter.CompanyName = "Flc Ltd";
                c.Exporter.Address = new DtosMmo.Address
                {
                    Line1 = "First line",
                    Line2 = "Second line",
                    City = "Town",
                    PostCode = "S1 5CD"
                };
                c.Landings = new List<DtosMmo.Landing>
                {
                    _fixture
                        .Build<DtosMmo.Landing>()
                        .With(l => l.Species, "COD" )
                        .With(l => l.ScientificName, "Codus Delicicus" )
                        .With(l => l.Weight, 100)
                        .Create(),
                    _fixture
                        .Build<DtosMmo.Landing>()
                        .With(l => l.Species, "HAD")
                        .With(l => l.ScientificName, "Haddock Expensificus")
                        .With(l => l.Weight, 50)
                        .Create(),
                };
            });

            _webApplicationFactory.CatchCertificateRepository
                .Setup(r => r.GetByDocumentNumberAsync(_existingCatchCert.DocumentNumber))
                .ReturnsAsync(() => _existingCatchCert);
        }

        [Fact]
        public async Task Process_UnknownDocumentNumber_BadRequest()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = new[] { "ABCD" },
                ResponseItems = new[] { new DtosEhco.ApplicationFormItem() }
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.BadRequest);

            content.Errors.Should().ContainKey("mmoDocumentNumbers");
            content.Errors["mmoDocumentNumbers"].Should().HaveCount(1).And.Contain(s => s.Contains("ABCD"));
        }

        [Fact]
        public async Task Process_EmptyQuestionList_BadRequest()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = new[] { _existingCatchCert.DocumentNumber },
                ResponseItems = []
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.BadRequest);

            content.Errors.Should().ContainKey("responseItems");
        }

        [Fact]
        public async Task Process_EmptyDocumentNumbersList_BadRequest()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = [],
                ResponseItems = new[] { new DtosEhco.ApplicationFormItem() }
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.BadRequest);

            content.Errors.Should().ContainKey("mmoDocumentNumbers");
        }

        [Fact]
        public async Task Process_MoreThanOneDocumentNumber_BadRequest()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = new[] { "GBR-2021-CC-100AAAAAA", "GBR-2021-CC-100AAAAAB" },
                ResponseItems = new[] { new DtosEhco.ApplicationFormItem() }
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

            Verify(content, HttpStatusCode.BadRequest);

            content.Errors.Should().ContainKey("mmoDocumentNumbers");
            content.Errors["mmoDocumentNumbers"].Should().HaveCount(1).And.Contain("Only 1 document number is supported.");
        }

        [Fact]
        public async Task Process_KnownDocumentNumber_Success()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = new[] { _existingCatchCert.DocumentNumber },
                ResponseItems = new[]
                {
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 1L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 142L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 164L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 166L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 167L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 165L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 168L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 169L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 99999L
                    }
                },
                OrganisationId = new Guid("1C4E19B5-82BD-41F4-8994-6104C37DC9C2")
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DtosEhco.EhcoMmoMappedResult>();

            content.Should().NotBeNull();

            content.ResponseItems.Should().HaveCount(9);
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 142L && r.Answer == "England");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 164L && r.Answer == "Dave Fish Flc Ltd");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 166L && r.PageOccurrence == 0 && r.Answer == "COD");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 166L && r.PageOccurrence == 1 && r.Answer == "HAD");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 167L && r.PageOccurrence == 0 && r.Answer == "100 KGM");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 167L && r.PageOccurrence == 1 && r.Answer == "50 KGM");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 165L && r.PageOccurrence == 0 && r.Answer == "150 KGM");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 168L && r.Answer == "Dave Fish Flc Ltd");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 169L && r.Answer == "First line, Second line, Town, S1 5CD");
        }

        [Fact]
        public async Task Process_KnownDocumentNumberUsingIncorrectOrgId_Success()
        {
            var client = _webApplicationFactory.CreateClient();

            var request = new DtosEhco.EhcoMmoMappingRequest
            {
                MmoDocumentNumbers = new[] { _existingCatchCert.DocumentNumber },
                ResponseItems = new[]
                {
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 1L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 142L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 164L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 166L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 167L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 165L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 168L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 169L
                    },
                    new DtosEhco.ApplicationFormItem
                    {
                        FormQuestionId = 99999L
                    }
                },
                OrganisationId = new Guid("2A2A22A2-82BD-41F4-8994-6104C37DC9C2")
            };

            var response = await client.PostAsJsonAsync($"ehco-mmo-mapping", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<DtosEhco.EhcoMmoMappedResult>();

            content.Should().NotBeNull();

            content.ResponseItems.Should().HaveCount(6);
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 142L && r.Answer == "England");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 166L && r.PageOccurrence == 0 && r.Answer == "COD");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 166L && r.PageOccurrence == 1 && r.Answer == "HAD");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 167L && r.PageOccurrence == 0 && r.Answer == "100 KGM");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 167L && r.PageOccurrence == 1 && r.Answer == "50 KGM");
            content.ResponseItems.Should().Contain(r => r.FormQuestionId == 165L && r.PageOccurrence == 0 && r.Answer == "150 KGM");
        }
    }
}