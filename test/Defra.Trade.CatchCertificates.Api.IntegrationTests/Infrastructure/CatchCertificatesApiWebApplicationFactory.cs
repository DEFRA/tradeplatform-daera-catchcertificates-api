// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.ExternalApi.Auditing.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.Infrastructure;

public class CatchCertificatesApiWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
{
    public Mock<ICatchCertificateCaseRepository> CatchCertificateRepository { get; set; }
    public Mock<IProcessingStatementRepository> ProcessingStatementRepository { get; set; }
    public Mock<IStorageDocumentRepository> StorageDocumentRepository { get; set; }
    public Mock<IProtectiveMonitoringService> ProtectiveMonitoringService { get; set; }
    public string ApiVersion { get; set; }

    public Mock<IAuditRepository> AuditRepository { get; set; }

    public CatchCertificatesApiWebApplicationFactory() : base()
    {
        ClientOptions.AllowAutoRedirect = false;
        CatchCertificateRepository = new Mock<ICatchCertificateCaseRepository>();
        ProcessingStatementRepository = new Mock<IProcessingStatementRepository>();
        StorageDocumentRepository = new Mock<IStorageDocumentRepository>();
        ProtectiveMonitoringService = new Mock<IProtectiveMonitoringService>();
        AuditRepository = new Mock<IAuditRepository>();
    }

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);

        client.BaseAddress = new Uri("https://localhost:5001");
        client.DefaultRequestHeaders.Add("x-api-version", ApiVersion);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public void AddApimUserContextHeaders(HttpClient client, Guid? clientId, string clientIPAddress)
    {
        if (clientId.HasValue)
        {
            client.DefaultRequestHeaders.Add("x-client-id", clientId.Value.ToString());
        }

        if (clientIPAddress != null)
        {
            client.DefaultRequestHeaders.Add("x-client-ipaddress", clientIPAddress);
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment(Environments.Production);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(CatchCertificateCaseSqlRepository));

            services.Remove(descriptor);

            services.AddSingleton<ICatchCertificateCaseRepository>(CatchCertificateRepository.Object);

            descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(ProcessingStatementSqlRepository));

            services.Remove(descriptor);

            services.AddSingleton<IProcessingStatementRepository>(ProcessingStatementRepository.Object);

            descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(StorageDocumentSqlRepository));

            services.Remove(descriptor);

            services.AddSingleton<IStorageDocumentRepository>(StorageDocumentRepository.Object);

            descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(ProtectiveMonitoringService));

            services.Remove(descriptor);

            services.AddSingleton<IProtectiveMonitoringService>(ProtectiveMonitoringService.Object);

            services.Replace(ServiceDescriptor.Singleton(AuditRepository.Object));
        });
    }
}