using Modelo.Tabelas;
using Persistencia.DAL.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Tabelas
{
    public class EstadoServico
    {
        private EstadoDAL estadoDAL = new EstadoDAL();

        public IQueryable<Estado> ObterEstadosClassificadosPorNome()
        {
            return estadoDAL.ObterEstadosClassificadosPorNome();
        }
    }
}
