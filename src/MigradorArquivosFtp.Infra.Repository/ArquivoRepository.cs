using Dapper;
using Microsoft.Extensions.Configuration;
using MigradorArquivosFtp.Domain.Interfaces;
using MigradorArquivosFtp.Domain.Model;
using MigradorArquivosFtp.Infra.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MigradorArquivosFtp.Infra.Repository
{
    public class ArquivoRepository : IDisposable, IArquivoRepository
    {
        private readonly IDbConnection Connection;
        private readonly IConfiguration _configuration;

        public ArquivoRepository(IConfiguration configuration,  IMySqlConnectionFactory connectionFactory)
        {
            Connection = connectionFactory.GetDbConnection();
            _configuration = configuration;
        }

        public IEnumerable<Arquivo> GetArquivos(int cod)
        {
            var sql = _configuration["Sqls:BuscaArquivos"];

            var itens = Connection.Query<Arquivo>(sql, new { cod });
            return itens.ToList();
        }
        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
