// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.CatchCertificates.Api.Infrastructure;
using Defra.Trade.Common.AppConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Defra.Trade.CatchCertificates.Api
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.ConfigureTradeAppConfiguration(opt =>
                    {
                        opt.UseKeyVaultSecrets = true;
                        opt.RefreshKeys.Add($"{CatchCertificatesSettings.OptionsName}:{nameof(CatchCertificatesSettings.Sentinel)}");
                        opt.Select<CatchCertificatesSettings>(CatchCertificatesSettings.OptionsName);
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
