using System.IO;

namespace MigradorArquivosFtp.Domain.Interfaces
{
    public interface IFtpService
    {
        Stream DownloadFileStream(string source);
        void UploadFileStream(Stream stream, string dest);
        string Remove(string source);
        bool IsFileExist(string path);
    }
}
