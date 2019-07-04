using Modelo.Tabelas;
using Persistencia.DAL.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Tabelas
{
    public class CidadeServico
    {
        private CidadeDAL cidadeDAL = new CidadeDAL();

        public IQueryable<Cidade> ObterCidadesPorEstado(long? estadoID)
        {
            return cidadeDAL.ObterCidadesPorEstado(estadoID);
        }
    }
}
