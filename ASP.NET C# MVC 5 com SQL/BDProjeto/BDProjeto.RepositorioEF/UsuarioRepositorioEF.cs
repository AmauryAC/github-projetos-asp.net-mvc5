using BDProjeto.Dominio;
using BDProjeto.Dominio.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProjeto.RepositorioEF
{
    public class UsuarioRepositorioEF : IRepositorio<Usuario>
    {
        private readonly BD bd;

        public UsuarioRepositorioEF()
        {
            bd = new BD();
        }

        public void Excluir(Usuario entidade)
        {
            var usuarioExcluir = bd.usuarios.First(x => x.Id == entidade.Id);

            bd.usuarios.Remove(usuarioExcluir);
            bd.SaveChanges();
        }

        public Usuario ListarPorId(string id)
        {
            return bd.usuarios.First(x => x.Id == int.Parse(id));
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return bd.usuarios;
        }

        public void Salvar(Usuario entidade)
        {
            if(entidade.Id > 0)
            {
                var usuarioAlterar = bd.usuarios.First(x => x.Id == entidade.Id);

                usuarioAlterar.Nome = entidade.Nome;
                usuarioAlterar.Cargo = entidade.Cargo;
                usuarioAlterar.Data = entidade.Data;
            }
            else
            {
                bd.usuarios.Add(entidade);
            }

            bd.SaveChanges();
        }
    }
}
