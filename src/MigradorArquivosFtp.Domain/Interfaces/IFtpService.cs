using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Domain.Interfaces
{
    public interface IFtpService
    {
        Stream DownloadFileStream(string source);
        void UploadFileStream(Stream stream, string dest);
        string Remove(string source);
    }
}
