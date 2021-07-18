using MigradorArquivosFtp.Domain.Factories.Interfaces;
using MigradorArquivosFtp.Domain.Helpers;
using MigradorArquivosFtp.Domain.Interfaces;
using MigradorArquivosFtp.Interfaces;
using System;
using System.IO;

namespace MigradorArquivosFtp.Runners
{
    public class MigradorRunner : IMigradorRunner
    {
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IFtpServiceFactory _factory;

        public MigradorRunner(IArquivoRepository arquivoRepository, IFtpServiceFactory factory)
        {
            _arquivoRepository = arquivoRepository;
            _factory = factory;
        }

        public void Run()
        {
            Console.Write("Digite o código do usuario para mover as imagens:");
            string readed = Console.ReadLine();

            int userCode = int.Parse(readed);


            var origin = _factory.GetOrigin();
            var destination = _factory.GetDestination();

            var arquivos = _arquivoRepository.GetArquivos(userCode);

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

                Console.WriteLine($"Arquivo copiado com sucesso: {path}");
            }
        }
    }
}
