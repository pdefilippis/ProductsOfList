using Ecommerce.Domain;
using Ecommerce.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Articulo = Ecommerce.Common.DataMembers.Input.Articulo;

namespace Ecommerce.Controllers
{
    public class ArticleController : Controller
    {

        private readonly Core.IArticuloManager _articuloManager;
        private readonly Core.IArticuloTipoManager _articuloTipoManager;
        private readonly Core.ILoteManager _loteManager;
        private readonly IConnectionContext _context;

        public ArticleController(Core.ILoteManager loteManager, Core.IArticuloManager articuloManager, Core.IArticuloTipoManager articuloTipoManager, IConnectionContext context)
        {
            _articuloManager = articuloManager;
            _articuloTipoManager = articuloTipoManager;
            _loteManager = loteManager;
            _context = context;
        }

        public IActionResult Index(int LotId)
        {
            var lote = _loteManager.Get();
            var item = lote.FirstOrDefault(l => l.Id == LotId);


            if (lote != null)
            {
                return View(new IndexPublicViewModel()
                {
                    Description = item.Descripcion,
                    LotId = item.Id
                });
            }
            else
                return RedirectToAction("Status", "Error", new { code = 404 });
        }

        public IActionResult IndexPublic()
        {
            return View();
        }

        public JsonResult GetArticles(int LotId)
        {
            var article = _articuloManager.GetAll();

            var items = article.Where(a => a.Lote.Id == LotId).Select(l => new
            {
                article_Description = l.Descripcion,
                brand = l.Marca,
                serialNumber = l.NumeroSerie,
                type = l.Tipo.Descripcion,
                state = l.Activo == true ? "Activo" : "Inactivo",
                article_id = l.Id,
                price = "$\n" + l.Precio.ToString(),
                adjudicated = l.UsuarioAdjudicado == null ? "Sin usuario" : l.UsuarioAdjudicado.UserName,
                userCount = l.UsuariosInteresados.Count
            }).ToList();

            return Json(items);
        }
        
        public IActionResult EnableDisable(int ArticleId, int LotId)
        {
            using (var context = _context.Get())
            {
                var article = context.Articulo.FirstOrDefault(u => u.Id == ArticleId);
                if (article == null)
                    return RedirectToAction("Index");
                else
                {
                    switch (article.Activo)
                    {
                        case true:
                            article.Activo = false;
                            break;

                        case false:
                            article.Activo = true;
                            break;
                    }
                }
                context.SaveChanges();
            }

            return RedirectToAction("Index", new { LotId = LotId });
        }

        [HttpGet]
        public IActionResult CreateArticle(int LotId, int ArticleId)
        {
            var article = _loteManager.Get();
            var article2 = _articuloTipoManager.Get();
            var article3 = _articuloManager.Get();


            if (article.Any(x => x.Id == LotId))
            {
                var model = new CreateArticleViewModel();

                model.LotId = LotId;
                var typeList = new List<SelectListItem>();

                foreach (var item in article2)
                {
                    typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                model.Types = typeList;

                if (ArticleId != 0)
                {
                    var item = article3.FirstOrDefault(x => x.Id == ArticleId);

                    if (article3 != null)
                    {
                        model.Price = item.Precio;
                        model.SerialNumber = item.NumeroSerie;
                        model.Description = item.Descripcion;
                        model.TypeId = item.Tipo.Id;
                        model.Brand = item.Marca;
                    }
                    else
                    {
                        return RedirectToAction("Index", new { LotId = LotId });
                    }
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", new { LotId = LotId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateArticle(CreateArticleViewModel vm)
        {
            var article2 = _articuloTipoManager.Get();

            if (ModelState.IsValid)
            {
                var article = new Articulo
                {
                    IdTipo = vm.TypeId,
                    NroSerie = vm.SerialNumber,
                    IdLote = vm.LotId,
                    Descripcion = vm.Description,
                    Precio = vm.Price
                    Marca = vm.Brand
                };

                _articuloManager.Save(article);

                return RedirectToAction("Index", new { lotId = article.IdLote });
            }
            else
            {
                var typeList = new List<SelectListItem>();

                foreach (var item in article2)
                {
                    typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                vm.Types = typeList;

                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult EditArticle(int ArticleId, int LotId)
        {
            var article = _articuloManager.GetById(ArticleId);
            var article2 = _articuloTipoManager.Get();

            if (article == null)
                return RedirectToAction("Index", new { LotId = LotId });
            else
            {
                var model = new EditArticleViewModel
                {
                    TypeId = article.Id,
                    Brand = article.Marca,
                    Description = article.Descripcion,
                    SerialNumber = article.NumeroSerie,
                    ArticleId = ArticleId,
                    Price = article.Precio,
                    Lot_ID = article.Lote.Id
                };

                var typeList = new List<SelectListItem>();

                foreach (var item in article2)
                {
                    typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                model.Types = typeList;

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArticle(EditArticleViewModel vm)
        {
            var article = _articuloManager.GetById(vm.ArticleId);
            var article2 = _articuloTipoManager.Get();

            if (ModelState.IsValid)
            {
                using (var context = _context.Get())
                {
                    var item = context.Articulo
                        .Include("IdLoteNavigation")
                        .Include("IdTipoNavigation")
                        .Where(x => x.Id.Equals(vm.ArticleId))
                        .FirstOrDefault();

                    item.IdTipo = vm.TypeId;
                    item.NumeroSerie = vm.SerialNumber;
                    item.Precio = vm.Price;
                    item.Descripcion = vm.Description;
                    item.Marca = vm.Brand;
                    
                    context.SaveChanges();
                }
                return RedirectToAction("Index", new { LotId = article.Lote.Id });
            }
            else
            {
                var typeList = new List<SelectListItem>();

                foreach (var item in article2)
                {
                    typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                vm.Types = typeList;

                return View(vm);
            }
        }

        //public IActionResult IndexPublic(int lotId)
        //{

        //    var lot = context.Lote.FirstOrDefault(l => l.Id == lotId && l.Estado == Lote.EstadoLote.ENABLED);
        //    if (lot != null)
        //    {
        //        var takenId = context.Lote.FirstOrDefault(l => l.Id == lotId).Articulo.FirstOrDefault(a => a.UserArticles.Any(x => x.UserId == CurrentUserId)) == null ? 0
        //        : context.Lote.FirstOrDefault(l => l.Id == lotId).Articulo.FirstOrDefault(a => a.UserArticles.Any(x => x.UserId == CurrentUserId)).Id;

        //        IndexPublicViewModel indexPublicViewModel = new IndexPublicViewModel
        //        {
        //            Descripcion = lot.Descripcion,
        //            TakenId = takenId,
        //            Id = lotId
        //        };

        //        return View(indexPublicViewModel);
        //    }

        //    return RedirectToAction("Status", "Error", new { code = 404 });
        //}

        //public JsonResult GetArticlesPublic(int lotId)
        //{
        //    var articleWon = context.Articulo.FirstOrDefault(x =>
        //    x.IdUsuarioAdjudicado == CurrentUserId &&
        //    x.IdLoteNavigation.UpdateDate.Year == DateTime.Now.Year);

        //    List<Articulo> articleList = context.Articulo.Where(a => a.IdLoteNavigation.Id == lotId && a.Estado == Articulo.ArticuloEstado.ENABLED).ToList();

        //    if (articleWon != null)
        //        articleList = articleList.Where(x => x.IdTipo != articleWon.IdTipo).ToList();

        //    var articles = articleList.Select(l => new
        //    {
        //        article_Description = l.Descripcion,
        //        serialNumber = l.NumeroSerie,
        //        type = l.IdTipoNavigation.Descripcion,
        //        article_id = l.Id,
        //        brand = l.Marca,
        //        userCount = l.UserArticles.Count,
        //        price = "$\n" + l.Precio.ToString()
        //    }).ToList();

        //    return Json(articles);
        //}


        //public IActionResult EnableDisable(int articleId, int lotId)
        //{
        //    var article = context.Articulo.FirstOrDefault(u => u.Id == articleId);
        //    if (article == null)
        //        return RedirectToAction("Index");
        //    else
        //    {
        //        switch (article.Estado)
        //        {
        //            case ArticuloEstado.NOT_ENABLED:
        //                article.Estado = ArticuloEstado.ENABLED;
        //                break;

        //            case ArticuloEstado.ENABLED:
        //                article.Estado = ArticuloEstado.NOT_ENABLED;
        //                break;
        //        }
        //    }
        //    context.SaveChanges();

        //    return RedirectToAction("Index", new { lotId = lotId });
        //}

        //public JsonResult ApplyUnApply(int articleId)
        //{
        //    var article = context.Articulo.FirstOrDefault(u => u.Id == articleId);
        //    var user = context.Usuario.FirstOrDefault(u => u.Id == CurrentUserId);
        //    var takenArticle = context.UserArticles.FirstOrDefault(x => x.ArticleId == articleId && x.UserId == CurrentUserId);

        //    if (article == null || user == null)
        //        return Json(false);
        //    else
        //    {
        //        if (takenArticle == null)
        //        {
        //            article.UserArticles.Add(new UserArticle { UserId = user.Id, ArticleId = articleId });
        //        }
        //        else
        //        {
        //            article.UserArticles.Remove(takenArticle);
        //        }
        //    }
        //    context.SaveChanges();

        //    return Json(true);
        //}

    }
}
