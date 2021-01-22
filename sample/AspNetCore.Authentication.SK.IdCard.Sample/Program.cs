using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace AspNetCore.Authentication.SK.IDCard.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenLocalhost(5000);
                        options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps());
                        options.ListenLocalhost(5002, listenOptions =>
                        {
                            listenOptions.UseHttps(adapterOptions =>
                            {
                                adapterOptions.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                            });
                        });
                    });
                });
    }
}
