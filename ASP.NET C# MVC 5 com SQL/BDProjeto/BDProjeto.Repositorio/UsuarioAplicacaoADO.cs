using BDProjeto.Dominio;
using BDProjeto.Dominio.Contrato;
using BDProjeto.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProjeto.RepositorioADO
{
    public class UsuarioAplicacaoADO : IRepositorio<Usuario>
    {
        private BD bd;

        private void Inserir(Usuario usuario)
        {
            var strQuery = "";

            strQuery += "INSERT INTO usuarios(nome, cargo, data)";
            strQuery += String.Format(" VALUES ('{0}', '{1}', '{2}')", usuario.Nome, usuario.Cargo, usuario.Data);

            // Destrói depois de executar
            using(bd = new BD())
            {
                bd.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Usuario usuario)
        {
            var strQuery = "";

            strQuery += "UPDATE usuarios SET ";
            strQuery += String.Format("Nome = '{0}',", usuario.Nome);
            strQuery += String.Format("Cargo = '{0}',", usuario.Cargo);
            strQuery += String.Format("Data = '{0}' ", usuario.Data);
            strQuery += String.Format("WHERE Id = '{0}'", usuario.Id);

            using(bd = new BD())
            {
                bd.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Usuario usuario)
        {
            if(usuario.Id > 0)
            {
                Alterar(usuario);
            }
            else
            {
                Inserir(usuario);
            }
        }

        public void Excluir(Usuario usuario)
        {
            var strQuery = "";

            strQuery += String.Format("DELETE FROM usuarios WHERE Id = '{0}'", usuario.Id);

            using(bd = new BD())
            {
                bd.ExecutaComando(strQuery);
            }
        }

        public Usuario ListarPorId(string id)
        {
            var strQuery = "";

            strQuery += String.Format("SELECT * FROM usuarios WHERE Id = '{0}'", id);

            using(bd = new BD())
            {
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            var strQuery = "SELECT * FROM usuarios";

            using(bd = new BD())
            {
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                return ReaderEmLista(retorno);
            }
        }

        private List<Usuario> ReaderEmLista(SqlDataReader reader)
        {
            List<Usuario> usuarios = new List<Usuario>();

            while(reader.Read())
            {
                var tempoObjeto = new Usuario()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Nome = reader["nome"].ToString(),
                    Cargo = reader["cargo"].ToString(),
                    Data = DateTime.Parse(reader["data"].ToString())
                };

                usuarios.Add(tempoObjeto);
            }

            return usuarios;
        }
    }
}
