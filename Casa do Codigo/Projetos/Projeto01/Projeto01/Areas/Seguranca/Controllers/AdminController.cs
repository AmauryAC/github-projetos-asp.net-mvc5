using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projeto01.Areas.Seguranca.Models;
using Projeto01.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Seguranca.Controllers
{
    public class AdminController : Controller
    {
        // GET: Seguranca/Admin
        [Authorize(Roles = "Administradores")]
        public ActionResult Index()
        {
            return View(GerenciadorUsuario.Users);
        }

        [Authorize(Roles = "Administradores")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsuarioViewModel model)
        {
            if(ModelState.IsValid)
            {
                Usuario user = new Usuario
                {
                    UserName = model.Nome,
                    Email = model.Email
                };

                IdentityResult result = GerenciadorUsuario.Create(user, model.Senha);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Administradores")]
        public ActionResult Edit(string id)
        {
            return ObterVisaoUsuarioPorId(id);
        }

        [HttpPost]
        public ActionResult Edit(UsuarioViewModel uvm)
        {
            if(ModelState.IsValid)
            {
                Usuario usuario = GerenciadorUsuario.FindById(uvm.Id);

                usuario.UserName = uvm.Nome;
                usuario.Email = uvm.Email;
                usuario.PasswordHash = GerenciadorUsuario.PasswordHasher.HashPassword(uvm.Senha);

                IdentityResult result = GerenciadorUsuario.Update(usuario);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(uvm);
        }

        [Authorize(Roles = "Administradores")]
        public ActionResult Details(string id)
        {
            return ObterVisaoUsuarioPorId(id);
        }

        [Authorize(Roles = "Administradores")]
        public ActionResult Delete(string id)
        {
            return ObterVisaoUsuarioPorId(id);
        }

        [HttpPost]
        public ActionResult Delete(Usuario usuario)
        {
            Usuario user = GerenciadorUsuario.FindById(usuario.Id);

            if(user != null)
            {
                IdentityResult result = GerenciadorUsuario.Delete(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        private GerenciadorUsuario GerenciadorUsuario {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult ObterVisaoUsuarioPorId(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = GerenciadorUsuario.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var uvm = new UsuarioViewModel();

            uvm.Id = usuario.Id;
            uvm.Nome = usuario.UserName;
            uvm.Email = usuario.Email;

            return View(uvm);
        }
    }
}