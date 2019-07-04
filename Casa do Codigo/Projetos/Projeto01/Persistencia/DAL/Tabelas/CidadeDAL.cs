using Modelo.Tabelas;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL.Tabelas
{
    public class CidadeDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Cidade> ObterCidadesPorEstado(long? estadoID)
        {
            return context.Cidades.Where(c => c.EstadoID == estadoID).OrderBy(c => c.Nome);
        }
    }
}
