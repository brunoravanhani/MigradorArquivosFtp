using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigradorArquivosFtp.Domain.Factories.Interfaces;
using MigradorArquivosFtp.Domain.Helpers;
using MigradorArquivosFtp.Domain.Interfaces;
using MigradorArquivosFtp.Infra.Repository;
using MigradorArquivosFtp.Infra.Repository.Factory;
using MigradorArquivosFtp.Infra.Repository.Interfaces;
using MigradorArquivosFtp.Infra.Services;
using MigradorArquivosFtp.Infra.Services.Factories;
using MigradorArquivosFtp.Interfaces;
using MigradorArquivosFtp.Runners;
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
                            .AddTransient<IArquivoRepository, ArquivoRepository>()
                            .AddTransient<IFtpServiceFactory, FtpServiceFactory>()
                            .AddTransient<IVerificadorRunner, VerificadorRunner>()
                           .AddTransient<IDeletadorRunner, DeletadorRunner>()
                           .AddTransient<IMigradorRunner, MigradorRunner>());
    
        static void Run(IServiceProvider serviceProvider)
        {
            Console.WriteLine(@"
1 - Verificador
2 - Deletador
3 - Migrador");
            Console.Write("Digite o código da opção desejada:");
            string op = Console.ReadLine();

            int option = int.Parse(op);
            IRunner runner = option switch
            {
                1 => serviceProvider.GetService<IVerificadorRunner>(),
                2 => serviceProvider.GetService<IDeletadorRunner>(),
                3 => serviceProvider.GetService<IMigradorRunner>(),
                _ => throw new Exception("Opção não encontrada"),
            };
            runner.Run();
            Environment.Exit(1);
        }
    }
}
