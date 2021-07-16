using Microsoft.Extensions.Configuration;
using MigradorArquivosFtp.Infra.Repository.Interfaces;
using MySqlConnector;
using System;
using System.Data;

namespace MigradorArquivosFtp.Infra.Repository.Factory
{
    public class MySqlConnectionFactory : IMySqlConnectionFactory
    {

        private readonly IConfiguration _configuration;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            var c = _configuration.GetConnectionString("MigradorContext");
            return new MySqlConnection(c);
        }
    }
}
