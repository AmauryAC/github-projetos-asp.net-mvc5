using IdentityLimpoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityLimpoMvc.Controllers
{
    [AllowAnonymous]
    public class AutenticacaoController : Controller
    {
        //private string GetRedirectUrl;

        // GET: Autenticacao
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            // Não faça isso em produção
            if(model.Login == "amauryac" && model.Senha == "123")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Amaury"),
                    new Claim(ClaimTypes.Email, "amauryac@hotmail.com"),
                    new Claim(ClaimTypes.Country, "Brasil")
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // Autenticação falhou
            ModelState.AddModelError("", "Usuário ou senha inválidos");

            return View();
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if(string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }
    }
}