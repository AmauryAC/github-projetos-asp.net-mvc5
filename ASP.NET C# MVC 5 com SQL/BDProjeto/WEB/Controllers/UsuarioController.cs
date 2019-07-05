using BDProjeto.Aplicacao;
using BDProjeto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioAplicacao appUsuario;

        public UsuarioController()
        {
            //appUsuario = UsuarioAplicacaoConstrutor.UsuarioApEF();
            appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

            var listaUsuarios = appUsuario.ListarTodos();

            return View(listaUsuarios);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

                appUsuario.Salvar(usuario);

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Edit(string id)
        {
            //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

            var usuario = appUsuario.ListarPorId(id);

            if(usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

                appUsuario.Salvar(usuario);

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Details(string id)
        {
            //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

            var usuario = appUsuario.ListarPorId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        public ActionResult Delete(string id)
        {
            //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

            var usuario = appUsuario.ListarPorId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //var appUsuario = UsuarioAplicacaoConstrutor.UsuarioApADO();

            var usuario = appUsuario.ListarPorId(id);

            appUsuario.Excluir(usuario);

            return RedirectToAction("Index");
        }
    }
}