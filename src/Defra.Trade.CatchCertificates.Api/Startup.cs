// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.Infrastructure;
using Defra.Trade.Common.Api.Infrastructure;
using Defra.Trade.Common.ExternalApi.ApimIdentity;
using Defra.Trade.Common.ExternalApi.Auditing;
using Defra.Trade.Common.Sql.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTradeApi(Configuration);
        services.AddTradeExternalApimIdentity(Configuration);
        services.AddTradeExternalAuditing(Configuration, "CommonAuditSql");
        services.AddTradeSql(Configuration);
        services.AddServiceRegistrations(Configuration);
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation("Starting {EnvironmentName} {ApplicationName} from {ContentRootPath}",
            env.EnvironmentName, env.ApplicationName, env.ContentRootPath);

        app.UseTradeExternalAuditing();
        app.UseTradeApp(env);
    }
}