using MigradorArquivosFtp.Domain.Factories.Interfaces;
using MigradorArquivosFtp.Domain.Helpers;
using MigradorArquivosFtp.Domain.Interfaces;
using MigradorArquivosFtp.Interfaces;
using System;

namespace MigradorArquivosFtp.Runners
{
    public class DeletadorRunner : IDeletadorRunner
    {
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IFtpServiceFactory _factory;

        public DeletadorRunner(IArquivoRepository arquivoRepository, IFtpServiceFactory factory)
        {
            _arquivoRepository = arquivoRepository;
            _factory = factory;
        }
        public void Run()
        {
            Console.Write("Digite o código do usuario para remover as imagens:");
            string readed = Console.ReadLine();

            int userCode = int.Parse(readed);


            var origin = _factory.GetOrigin();

            var arquivos = _arquivoRepository.GetArquivos(userCode);

            foreach (var item in arquivos)
            {
                var path = DirectoryFileHelper.ResolvePathImoveis(item.Imagem, item.CodUser, item.Legenda);
                try
                {
                    string response = origin.Remove(path);
                    Console.WriteLine($"Removendo arquivo {path} - Status: {response}");
                    System.Threading.Thread.Sleep(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed: {path} - Erro: {e.Message}");
                }


            }
        }

    }
}
