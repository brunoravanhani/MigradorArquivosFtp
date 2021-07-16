using MigradorArquivosFtp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Domain.Factories.Interfaces
{
    public interface IFtpServiceFactory
    {
        IFtpService GetOrigin();
        IFtpService GetDestination();

    }
}
