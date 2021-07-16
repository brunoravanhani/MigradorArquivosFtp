using MigradorArquivosFtp.Domain.Interfaces;
using System;
using System.IO;
using System.Net;
using static System.Net.WebRequestMethods;

namespace MigradorArquivosFtp.Infra.Services
{
    public class FtpService : IFtpService
    {
        private readonly string FtpUser;
        private readonly string FtpPassword;
        private readonly string FtpUrlBase;

        public FtpService(string user, string password, string urlBase)
        {
            FtpUser = user;
            FtpPassword = password;
            FtpUrlBase = urlBase;
        }

        public Stream DownloadFileStream(string path)
        {
            var source = FtpUrlBase + path;

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(source);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = Ftp.DownloadFile;
            ftpRequest.Credentials = new NetworkCredential(FtpUser, FtpPassword);

            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

            Stream responseStream = response.GetResponseStream();
            return responseStream;
        }

        public void UploadFileStream(Stream stream, string path)
        {
            var dest = FtpUrlBase + path;

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(dest);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = Ftp.UploadFile;
            ftpRequest.Credentials = new NetworkCredential(FtpUser, FtpPassword);

            using (System.IO.Stream requestStream = ftpRequest.GetRequestStream())
            {
                stream.CopyTo(requestStream);
            }

        }

        public string Remove(string path)
        {
            var source = FtpUrlBase + path;

            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(source);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = Ftp.DeleteFile;
            ftpRequest.Credentials = new NetworkCredential(FtpUser, FtpPassword);

            using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
            {
                return response.StatusDescription;
            }
        }
    }
}
