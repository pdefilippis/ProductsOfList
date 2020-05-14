using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Domain;
using Ecommerce.ViewModels.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Articulo = Ecommerce.Common.DataMembers.Input.Articulo;

namespace Ecommerce.Controllers
{
    public class ArticleController : Controller
    {

        private readonly Core.IArticuloManager _articuloManager;
        private readonly Core.IArticuloTipoManager _articuloTipoManager;
        private readonly Core.ILoteManager _loteManager;
        private readonly IConnectionContext _context;
        private readonly Core.IUsuarioManager _usuarioManager;
        private readonly IHttpContextAccessor httpContextAccessor;

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
                    Precio = vm.Price,
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

        public JsonResult GetArticlesPublic(int lotId)
        {
            var CurrentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var article = _articuloManager.GetAll();
            var articleWon = _articuloManager.Get().FirstOrDefault(x =>
                x.UsuarioAdjudicado.Id == CurrentUserId &&
                x.Lote.Actualizacion.Year == DateTime.Now.Year);

            var articleList = article.Where(a => a.Lote.Id == lotId && a.Activo == true).ToList();

            if (articleWon != null)
                articleList = articleList.Where(x => x.Tipo.Id != articleWon.Tipo.Id).ToList();

            var articles = articleList.Select(l => new
            {
                article_Description = l.Descripcion,
                serialNumber = l.NumeroSerie,
                type = l.Lote.Descripcion,
                article_id = l.Id,
                brand = l.Marca,
                price = "$\n" + l.Precio.ToString(),
                userCount = l.UsuariosInteresados.Count
            }).ToList();

            return Json(articles);
        }

        public IActionResult IndexPublic(int lotId)
        {
            var CurrentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var lot = _loteManager.Get().FirstOrDefault(l => l.Id == lotId && l.Activo == true);
            var takenId = _loteManager.Get().FirstOrDefault(l => l.Id == lotId).Articulos.FirstOrDefault(a => a.UsuariosInteresados.Any(x => x.Id == CurrentUserId)) == null ? 0
                : _loteManager.Get().FirstOrDefault(l => l.Id == lotId).Articulos.FirstOrDefault(a => a.UsuariosInteresados.Any(x => x.Id == CurrentUserId)).Id;
            if (lot != null)
            {
                IndexPublicViewModel indexPublicViewModel = new IndexPublicViewModel
                {
                    Description = lot.Descripcion,
                    TakenId = takenId,
                    LotId = lotId
                };

                return View(indexPublicViewModel);
            }

            return RedirectToAction("Status", "Error", new { code = 404 });
        }

        public JsonResult ApplyUnApply(int articleId)
        {
            var CurrentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var article = _articuloManager.Get().FirstOrDefault(u => u.Id == articleId);
            var user = _usuarioManager.Get().FirstOrDefault(u => u.Id == CurrentUserId);
            var takenArticle = _articuloManager.Get().FirstOrDefault(x => x.Id == articleId && x.UsuarioAdjudicado.Id == CurrentUserId);

            if (article == null || user == null)
            {
                return Json(false);
            }
            else
            {
                if (takenArticle == null)
                {
                    _articuloManager.PostularArticulo(new ArticuloPostulacion
                    {
                        IdArticulo = articleId,
                        IdUsuario = user.Id
                    });
                }
                else
                {
                    _articuloManager.DeclinarPostulacionArticulo(new ArticuloPostulacion
                    {
                        IdArticulo = articleId,
                        IdUsuario = user.Id
                    });
                }
            }

            return Json(true);
        }

    }
}
