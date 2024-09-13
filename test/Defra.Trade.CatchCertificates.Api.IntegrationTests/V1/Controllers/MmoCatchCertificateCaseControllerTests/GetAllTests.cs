// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using Defra.Trade.Common.Api.Dtos;
using Moq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.V1.Controllers.MmoCatchCertificateCaseControllerTests
{
    public class GetAllTests : IClassFixture<CatchCertificatesApiWebApplicationFactory<Startup>>
    {
        private readonly CatchCertificatesApiWebApplicationFactory<Startup> _webApplicationFactory;
        private readonly Fixture _fixture;

        public GetAllTests(CatchCertificatesApiWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _webApplicationFactory.ApiVersion = "1-internal";

            _fixture = new Fixture();

            _webApplicationFactory.CatchCertificateRepository
                .Setup(r => r.GetPaginatedAsync(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(
                    (long pageNumber, long pageSize) =>
                    {
                        return Tuple.Create(
                            _fixture.CreateMany<Models.CatchCertificateCaseDataRow>((int)pageSize),
                            15L);
                    });
        }

        [Fact]
        public async Task Get_WithPagination_Success()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("mmo-catch-certificate-case?pageSize=10&pageNumber=1");

            var content = await response.Content.ReadAsAsync<PagedResult<CatchCertificateCaseEntry>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(10, content.Data.Count());
            Assert.Equal(10, content.Records);
            Assert.Equal(1, content.PageNumber);
            Assert.Equal(10, content.PageSize);
            Assert.Equal(2, content.TotalPages);
            Assert.Equal(15, content.TotalRecords);
        }
    }
}