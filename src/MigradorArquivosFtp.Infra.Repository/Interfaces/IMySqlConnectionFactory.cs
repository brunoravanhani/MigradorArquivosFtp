
using System.Data;


namespace MigradorArquivosFtp.Infra.Repository.Interfaces
{
    public interface IMySqlConnectionFactory
    {
        IDbConnection GetDbConnection();
    }
}
