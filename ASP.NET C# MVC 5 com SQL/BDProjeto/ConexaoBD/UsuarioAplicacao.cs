using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoBD
{
    public class UsuarioAplicacao
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
            strQuery += String.Format("WHERE usuarioId = '{0}'", usuario.Id);

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

        public void Excluir(int id)
        {
            var strQuery = "";

            strQuery += String.Format("DELETE FROM usuarios WHERE usuarioId = '{0}'", id);

            using(bd = new BD())
            {
                bd.ExecutaComando(strQuery);
            }
        }

        public List<Usuario> ListarTodos()
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
                    Id = int.Parse(reader["usuarioId"].ToString()),
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
