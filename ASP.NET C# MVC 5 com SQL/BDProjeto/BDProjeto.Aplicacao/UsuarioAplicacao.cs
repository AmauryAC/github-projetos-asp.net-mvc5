using BDProjeto.Dominio;
using BDProjeto.Dominio.Contrato;
using BDProjeto.Repositorio;
using BDProjeto.RepositorioADO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProjeto.Aplicacao
{
    public class UsuarioAplicacao
    {
        private readonly IRepositorio<Usuario> repositorio;

        public UsuarioAplicacao(IRepositorio<Usuario> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Usuario usuario)
        {
            repositorio.Salvar(usuario);
        }

        public void Excluir(Usuario usuario)
        {
            repositorio.Excluir(usuario);
        }

        public Usuario ListarPorId(string id)
        {
            return repositorio.ListarPorId(id);
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return repositorio.ListarTodos();
        }
    }
}
