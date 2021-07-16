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
                            .AddTransient<IFtpServiceFactory, FtpServiceFactory>());
    
        static void Run(IServiceProvider serviceProvider)
        {
            Console.Write("Digite o código do usuario para excluir as imagens:");
            string readed = Console.ReadLine();

            int userCode = int.Parse(readed);


            var rep = serviceProvider.GetService<IArquivoRepository>();
            var factory = serviceProvider.GetService<IFtpServiceFactory>();
            var origin = factory.GetOrigin();
            var destination = factory.GetDestination();

            var arquivos = rep.GetArquivos(userCode);

            foreach (var item in arquivos)
            {
                var path = DirectoryFileHelper.ResolvePathImoveis(item.Imagem, item.CodUser, item.Legenda);

                Stream stream = null;

                try
                {
                    stream = origin.DownloadFileStream(path);

                }
                catch (Exception)
                {
                    Console.WriteLine($"Erro ao buscar arquivo: {path}");
                }

                if (stream != null)
                    destination.UploadFileStream(stream, path);
            }
        }
    }
}
