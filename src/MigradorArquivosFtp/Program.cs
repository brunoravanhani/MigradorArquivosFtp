using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigradorArquivosFtp.Domain.Interfaces;
using MigradorArquivosFtp.Infra.Repository;
using MigradorArquivosFtp.Infra.Repository.Factory;
using MigradorArquivosFtp.Infra.Repository.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MigradorArquivosFtp
{
    class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            Run(host.Services);
            
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, configuration) =>
               {
                   configuration.Sources.Clear();

                   configuration
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                   var config = configuration.Build();

               })
               .ConfigureServices((_, services) =>
                   services.AddTransient<IMySqlConnectionFactory, MySqlConnectionFactory>()
                            .AddTransient<IArquivoRepository, ArquivoRepository>());
    
        static void Run(IServiceProvider serviceProvider)
        {
            var rep = serviceProvider.GetService<IArquivoRepository>();

            var arquivos = rep.GetArquivos(386);
        }
    }
}
