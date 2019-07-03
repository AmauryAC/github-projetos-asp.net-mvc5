using Modelo.Cadastros;
using Servicos.Cadastros;
using Servicos.Tabelas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Produtos
        [Authorize(Roles = "Administradores")]
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        // GET: Produtos/Details/5
        [Authorize(Roles = "Administradores")]
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // GET: Produtos/Create
        [Authorize(Roles = "Administradores")]
        public ActionResult Create()
        {
            PopularViewBag();

            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            return GravarProduto(produto, logotipo, chkRemoverImagem);
        }

        // GET: Produtos/Edit/5
        [Authorize(Roles = "Administradores")]
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));

            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            return GravarProduto(produto, logotipo, chkRemoverImagem);
        }

        // GET: Produtos/Delete/5
        [Authorize(Roles = "Administradores")]
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);

                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((long)id);

            if(produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if(produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase logotipo, string chkRemoverImagem)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(chkRemoverImagem != null)
                    {
                        produto.Logotipo = null;
                    }
                    if((logotipo != null) && (logotipo.ContentType == "image/jpeg" || logotipo.ContentType == "image/png"))
                    {
                        produto.LogotipoMimeType = logotipo.ContentType;
                        produto.Logotipo = SetLogotipo(logotipo);

                        produto.NomeArquivo = logotipo.FileName;
                        produto.TamanhoArquivo = logotipo.ContentLength;
                    }

                    produtoServico.GravarProduto(produto);

                    return RedirectToAction("Index");
                }

                PopularViewBag(produto);

                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);

                return View(produto);
            }
        }

        private byte[] SetLogotipo(HttpPostedFileBase logotipo)
        {
            var bytesLogotipo = new byte[logotipo.ContentLength];

            logotipo.InputStream.Read(bytesLogotipo, 0, logotipo.ContentLength);

            return bytesLogotipo;
        }

        public FileContentResult GetLogotipo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);

            if(produto != null)
            {
                return File(produto.Logotipo, produto.LogotipoMimeType);
            }

            return null;
        }

        public ActionResult DownloadArquivo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);

            FileStream fileStream = new FileStream(Server.MapPath("~/TempData/" + produto.NomeArquivo), FileMode.Create, FileAccess.Write);

            fileStream.Close();

            return File(fileStream.Name, produto.LogotipoMimeType, produto.NomeArquivo);
        }
    }
}
