using Introducao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Introducao.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var pessoa = new Pessoa();

            pessoa.Id = 1;
            pessoa.Nome = "Amaury A. Costa";
            pessoa.Tipo = "Administrador";

            // Usando ViewData
            ViewData["PessoaId"] = pessoa.Id;
            ViewData["Nome"] = pessoa.Nome;
            ViewData["Tipo"] = pessoa.Tipo;

            // Usando ViewBag
            ViewBag.PessoaId = pessoa.Id;
            ViewBag.Nome = pessoa.Nome;
            ViewBag.Tipo = pessoa.Tipo;

            // Usando View Tipada
            return View(pessoa);
        }

        public ActionResult Contatos()
        {
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Lista(int PessoaId, string Nome, string Tipo)
        {
            ViewData["Id"] = PessoaId;
            ViewData["Nome"] = Nome;
            ViewData["Tipo"] = Tipo;

            return View();
        }*/

        /*[HttpPost]
        public ActionResult Lista(FormCollection form)
        {
            ViewData["Id"] = form["PessoaId"];
            ViewData["Nome"] = form["Nome"];
            ViewData["Tipo"] = form["Tipo"];

            return View();
        }*/

        /*[HttpPost]
        public ActionResult Lista(Pessoa pessoa)
        {
            ViewData["Id"] = pessoa.Id;
            ViewData["Nome"] = pessoa.Nome;
            ViewData["Tipo"] = pessoa.Tipo;

            return View();
        }*/

        [HttpPost]
        public ActionResult Lista(Pessoa pessoa)
        {
            return View(pessoa);
        }
    }
}