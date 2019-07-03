using Modelo.Cadastros;
using Servicos.Cadastros;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class FabricantesController : Controller
    {
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Fabricantes
        [Authorize(Roles = "Administradores")]
        public ActionResult Index()
        {
            return View(fabricanteServico.ObterFabricantesClassificadosPorNome());
        }

        // GET: Create
        [Authorize(Roles = "Administradores")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        // GET: Edit
        [Authorize(Roles = "Administradores")]
        public ActionResult Edit(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        // GET: Details
        [Authorize(Roles = "Administradores")]
        public ActionResult Details(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        // GET: Delete
        [Authorize(Roles = "Administradores")]
        public ActionResult Delete(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Fabricante fabricante = fabricanteServico.EliminarFabricantePorId(id);

                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = fabricanteServico.ObterFabricantePorId((long)id);

            if(fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);
        }

        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fabricanteServico.GravarFabricante(fabricante);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(fabricante);
                }
            }
            catch
            {
                return View(fabricante);
            }
        }
    }
}