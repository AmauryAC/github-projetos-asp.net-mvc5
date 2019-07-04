using Modelo.Tabelas;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL.Tabelas
{
    public class EstadoDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Estado> ObterEstadosClassificadosPorNome()
        {
            return context.Estados.OrderBy(b => b.Nome);
        }
    }
}
