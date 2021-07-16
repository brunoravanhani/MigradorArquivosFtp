using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigradorArquivosFtp.Domain.Helpers
{
    public static class DirectoryFileHelper
    {
        public static string ResolvePathImoveis(string imagem, string codUser, string legenda)
        {

            if (legenda == "userpath")
            {
                return $"upload/imoveis/n{codUser.ToString().PadLeft(6, '0')}/{imagem}";
            }
            else
            if (legenda == "rootpath")
            {
                return "upload/imoveis/" + imagem;
            }

            return null;
        }
    }
}
