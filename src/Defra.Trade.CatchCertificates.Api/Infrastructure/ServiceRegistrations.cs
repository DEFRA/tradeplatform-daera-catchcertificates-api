// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Sql.Infrastructure;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.CatchCertificates.Api.Infrastructure;

public static class ServiceRegistrations
{
    public static IServiceCollection AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddSqlServer();

        services
            .AddAutoMapper(typeof(Startup))
            .Configure<CatchCertificatesSettings>(configuration.GetSection(CatchCertificatesSettings.OptionsName));

        return services
            .AddMmoRegistrations()
            .AddProtectiveMonitoringRegistrations(configuration);
    }

    private static IServiceCollection AddMmoRegistrations(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient)
            .AddScoped<ICatchCertificateCaseRepository, CatchCertificateCaseSqlRepository>()
            .AddScoped<IProcessingStatementRepository, ProcessingStatementSqlRepository>()
            .AddScoped<IStorageDocumentRepository, StorageDocumentSqlRepository>()
            .AddScoped<IProtectiveMonitoringService, ProtectiveMonitoringService>();
    }

    private static IServiceCollection AddProtectiveMonitoringRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProtectiveMonitoring(
            configuration,
            options => configuration.Bind(ProtectiveMonitoringSettings.OptionsName, options));

        return services;
    }
}
