using Microsoft.Extensions.Configuration;
using MigradorArquivosFtp.Domain.Factories.Interfaces;
using MigradorArquivosFtp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Infra.Services.Factories
{
    public class FtpServiceFactory : IFtpServiceFactory
    {
        private readonly IConfiguration _configuration;
        public FtpServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IFtpService GetDestination()
        {
            var url = _configuration["FtpDestination:Url"];
            var user = _configuration["FtpDestination:User"];
            var password = _configuration["FtpDestination:Password"];

            return new FtpService(user, password, url);
        }

        public IFtpService GetOrigin()
        {
            var url = _configuration["FtpOrigin:Url"];
            var user = _configuration["FtpOrigin:User"];
            var password = _configuration["FtpOrigin:Password"];

            return new FtpService(user, password, url);
        }
    }
}
