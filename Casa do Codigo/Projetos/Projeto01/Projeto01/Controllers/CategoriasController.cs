using Projeto01.Contexts;
using Projeto01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Controllers
{
    public class CategoriasController : Controller
    {
        EFContext context = new EFContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            try
            {
                context.Categorias.Add(categoria);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Edit
        public ActionResult Edit(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Fabricante").First();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(categoria).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }
        }

        // GET: Details
        public ActionResult Details(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Fabricante").First();

            if(categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        // GET: Delete
        public ActionResult Delete(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Fabricante").First();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Categoria categoria = context.Categorias.Find(id);

                context.Categorias.Remove(categoria);
                context.SaveChanges();

                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}