﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projeto01.Areas.Seguranca.Models;
using Projeto01.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Seguranca.Controllers
{
    public class AccountController : Controller
    {
        // GET: Seguranca/Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                Usuario user = UserManager.Find(details.Nome, details.Senha);

                if(user == null)
                {
                    ModelState.AddModelError("", "Nome ou senha inválido(s).");
                }
                else
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);

                    if (returnUrl == null)
                        returnUrl = "/Home";

                    return Redirect(returnUrl);
                }
            }

            return View(details);
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}