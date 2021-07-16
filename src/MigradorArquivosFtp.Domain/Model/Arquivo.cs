using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Domain.Model
{
    public class Arquivo
    {
        public int Cod { get; set; }
        public string Imagem { get; set; }
        public string Legenda { get; set; }
        public string CodUser { get; set; }
    }
}
