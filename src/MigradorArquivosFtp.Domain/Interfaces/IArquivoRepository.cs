using MigradorArquivosFtp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Domain.Interfaces
{
    public interface IArquivoRepository
    {
        IEnumerable<Arquivo> GetArquivos(int cod);
    }
}
