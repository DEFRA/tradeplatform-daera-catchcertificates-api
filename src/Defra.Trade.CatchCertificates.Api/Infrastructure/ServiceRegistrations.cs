// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Mappers;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.Common.Sql.Infrastructure;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DtosOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;
using ValOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Validation.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Infrastructure
{
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
                .AddEhcoMappingRegistrations()
                .AddProtectiveMonitoringRegistrations(configuration);
        }

        private static IServiceCollection AddEhcoMappingRegistrations(this IServiceCollection services)
        {
            return services
                .AddTransient<IValidator<DtosOutboundEhco.EhcoMmoMappingRequest>, ValOutboundEhco.EhcoMmoMappingRequestValidator>()
                .AddTransient<IValidator<DtosOutboundEhco.ApplicationFormItem>, ValOutboundEhco.ApplicationFormItemValidator>()
                .AddScoped<IEhcoMmoMappingService, EhcoMmoMappingService>()
                .AddScoped<IEhcoQuestionAnswerMapper, EhcoQuestionAnswerMapper>()
                .AddScoped<IEhcoMmoQuestionMappingRepository, EhcoMmoQuestionMappingSqlRepository>();
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
}