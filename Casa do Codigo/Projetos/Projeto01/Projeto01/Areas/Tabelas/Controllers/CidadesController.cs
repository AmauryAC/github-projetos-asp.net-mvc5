using Servicos.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Tabelas.Controllers
{
    public class CidadesController : Controller
    {
        private CidadeServico cidadeServico = new CidadeServico();

        public JsonResult GetCidadesDoEstado(string estadoID)
        {
            var cidades = cidadeServico.ObterCidadesPorEstado(Convert.ToInt32(estadoID));

            return Json(cidades, JsonRequestBehavior.AllowGet);
        }
    }
}