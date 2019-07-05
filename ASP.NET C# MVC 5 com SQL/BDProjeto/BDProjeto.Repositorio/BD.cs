using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProjeto.Repositorio
{
    public class BD : IDisposable
    {
        private readonly SqlConnection conexao;

        public BD()
        {
            conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["conexaoBD"].ConnectionString);
            conexao.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new SqlCommand()
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = conexao   
            };

            cmdComando.ExecuteNonQuery();
        }

        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new SqlCommand()
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = conexao
            };

            return cmdComando.ExecuteReader();
        }

        public void Dispose()
        {
            if(conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
