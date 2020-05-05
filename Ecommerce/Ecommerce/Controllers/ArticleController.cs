using Ecommerce.Domain.Models;
using Ecommerce.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Articulo = Ecommerce.Common.DataMembers.Input.Articulo;

namespace Ecommerce.Controllers
{
    public class ArticleController : Controller
    {
    
        private readonly Core.IArticuloManager _articuloManager;
        private readonly ProductsManagerContext context;
        private readonly Core.IArticuloTipoManager _articuloTipoManager;

        public ArticleController(ProductsManagerContext productsManagerContext, Core.IArticuloManager articuloManager, Core.IArticuloTipoManager articuloTipoManager)
        {
            this.context = productsManagerContext;
            _articuloManager = articuloManager;
            _articuloTipoManager = articuloTipoManager;
        }
        
        public JsonResult GetArticles(int lotId)
        {
            var article = _articuloManager.GetAll();

            var items = article.Where(a => a.Tipo.Id == lotId).Select(l => new
            {
                article_Description = l.Descripcion,
                serialNumber = l.NumeroSerie,
                type = l.Tipo.Descripcion,
                state = l.Activo == true ? "Activado" : "Desactivado",
                article_id = l.Id,
                price = "$\n" + l.Precio.ToString(),
                adjudicated = l.UsuarioAdjudicado == null ? "Sin usuario" : l.UsuarioAdjudicado
            }).ToList();

            return Json(items);
        }
        
        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View(new CreateArticleViewModel()
            {
                Types = _articuloTipoManager.Get().Select(x => new SelectListItem { Text = x.Descripcion, Value = x.Id.ToString() }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateArticle(CreateArticleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var article = new Articulo
                {
                    IdTipo = vm.TypeId,
                    NroSerie = vm.SerialNumber,
                    //Marca = vm.Brand, TODO => Hay que agregar el campo a la base
                    IdLote = vm.LotId,
                    Descripcion = vm.Description,
                    Precio = vm.Price
                };

                _articuloManager.Save(article);

                return RedirectToAction("Index", new { lotId = article.IdLote });                      
            }
            return View(vm);
        }
        
        
        
        /*[HttpGet]
        public IActionResult CreateArticle(int lotId, int articleId)
        {
            
            if (context.Lote.Any(x => x.Id == lotId == x.Activo == true))
            {
            var model = new CreateArticleViewModel();

            model.LotId = lotId;

            var typeList = new List<SelectListItem>();

            foreach (var item in context.ArticuloTipo)
            {
                typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
            }
            model.Types = typeList;

            if (articleId != 0)
            {
                var article = context.Articulo.FirstOrDefault(a => a.Id == articleId);
                
                if(article != null)
                {
                    model.Price = Convert.ToInt32(Decimal.ToInt32(article.Precio));
                    model.SerialNumber = article.NumeroSerie;
                    model.Description = article.Descripcion;
                    model.TypeId = article.IdTipo;
                }
                else
                {
                    return RedirectToAction("Index", new { lotId = lotId });
                }
            }
            return View(model);
        }
            else
                return RedirectToAction("Index", new { lotId = lotId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateArticle(CreateArticleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var article = new Articulo
                {
                    IdTipo = vm.TypeId,
                    NroSerie = vm.SerialNumber,
                    //Marca = vm.Brand, TODO => Hay que agregar el campo a la base
                    IdLote = vm.LotId,
                    Descripcion = vm.Description,
                    Precio = Convert.ToInt32(Decimal.ToInt32(vm.Price))
                };

                _articuloManager.Save(article);

                return RedirectToAction("Index", new { lotId = article.IdLote });
            }
            else
            {
                var typeList = new List<SelectListItem>();
                foreach (var item in context.ArticuloTipo)
                {
                    typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                vm.Types = typeList;

                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult EditArticle(int articleId, int lotId)
        {
            if (context.Lote.Any(x => x.Id == lotId))
            {
                var article = _articuloManager.GetById(articleId);

                if(article == null)
                    return RedirectToAction("Index", new { lotId = lotId });
                else
                {
                    var model = new EditArticleViewModel
                    {
                        TypeId = article.Id,
                        //Brand = article.Marca,
                        Description = article.Descripcion,
                        SerialNumber = article.NumeroSerie,
                        ArticleId = articleId,
                        Price = Convert.ToInt32(Decimal.ToInt32(article.Precio))
                    };

                    var typeList = new List<SelectListItem>();

                    foreach (var item in context.ArticuloTipo)
                    {
                        typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                    }
                    model.Types = typeList;

                    return View(model);
                }
            }
            else
                return RedirectToAction("Index", new { lotId = lotId });
        }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditArticle(EditArticleViewModel vm)
    {
        var article = context.Articulo.FirstOrDefault(u => u.Id == vm.ArticleId);

        if (ModelState.IsValid)
        {
            if (article != null)
            {
                article.IdTipo = vm.TypeId;
                //article.Marca = vm.Brand;
                article.Descripcion = vm.Description;
                article.NumeroSerie = vm.SerialNumber;
                article.Precio = (int)vm.Price;
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { lotId = article.IdLoteNavigation.Id });
        }
        else
        {
            var typeList = new List<SelectListItem>();

            foreach (var item in context.ArticuloTipo)
            {
                typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
            }
            vm.Types = typeList;

            return View(vm);
        }
    }*/
        
        
        public IActionResult Index(int lotId)
        {
            var lot = context.Lote.FirstOrDefault(l => l.Id == lotId == l.Activo == true);

            if (lot != null)
            {
                return View(new IndexPublicViewModel()
                {
                    Description = lot.Descripcion,
                    LotId = lot.Id
                });
            }

            return View();
        }
        
        public IActionResult IndexPublic()
        {
            return View();
        }
        
        //public IActionResult Index(int lotId)
        //{
        //    var lot = context.Lote.FirstOrDefault(l => l.Id == lotId);

        //    if (lot != null)
        //    {
        //        return View(new IndexPublicViewModel()
        //        {
        //            Descripcion = lot.Descripcion,
        //            Id = lot.Id,
        //            Estado = lot.Estado,
        //        });
        //    }
        //    else
        //        return RedirectToAction("Status", "Error", new { code = 404 });
        //}


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



        //public JsonResult GetArticles(int lotId)
        //{
        //    var article = context.Articulo.Where(a => a.IdLoteNavigation.Id == lotId).Select(l => new
        //    {
        //        article_Description = l.Descripcion,
        //        state = localizer[l.Estado.GetAttribute<DisplayAttribute>().Name].Value,
        //        serialNumber = l.NumeroSerie,
        //        type = l.IdTipoNavigation.Descripcion,
        //        article_id = l.Id,
        //        brand = l.Marca,
        //        price = "$\n" + l.Precio.ToString(),
        //        adjudicated = l.UsuarioAdjudicado.Usuario1.ToString() == null ? "Sin Usuario" : l.UsuarioAdjudicado.Usuario1.ToString(),
        //        userCount = l.UserArticles.Count
        //    }).ToList();

        //    return Json(article);
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


        //[HttpGet]
        //public IActionResult CreateArticle(int lotId, int articleId)
        //{
        //    if (context.Lote.Any(x => x.Id == lotId))
        //    {
        //        var model = new CreateArticleViewModel();

        //        model.LotId = lotId;
        //        var typeList = new List<SelectListItem>();

        //        foreach (var item in context.ArticuloTipos)
        //        {
        //            typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
        //        }
        //        model.Types = typeList;

        //        if (articleId != 0)
        //        {
        //            var article = context.Articulo.FirstOrDefault(a => a.Id == articleId);

        //            if (article != null)
        //            {
        //                model.Price = Convert.ToInt32(Decimal.ToInt32(article.Precio));
        //                model.SerialNumber = article.NumeroSerie;
        //                model.Description = article.Descripcion;
        //                model.Brand = article.Marca;
        //                model.TypeId = article.IdTipo;
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", new { lotId = lotId });
        //            }
        //        }

        //        return View(model);
        //    }
        //    else
        //        return RedirectToAction("Index", new { lotId = lotId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateArticle(CreateArticleViewModel createArticleViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var article = new Articulo
        //        {
        //            IdTipo = createArticleViewModel.TypeId,
        //            Marca = createArticleViewModel.Brand,
        //            NumeroSerie = createArticleViewModel.SerialNumber,
        //            Estado = Articulo.ArticuloEstado.ENABLED,
        //            IdLote = createArticleViewModel.LotId,
        //            Descripcion = createArticleViewModel.Description,
        //            UserArticles = new List<UserArticle>(),
        //            Precio = (int)createArticleViewModel.Price,
        //        };

        //        context.Articulo.Add(article);
        //        context.SaveChanges();

        //        return RedirectToAction("Index", new { lotId = article.IdLote });
        //    }
        //    else
        //    {

        //        var typeList = new List<SelectListItem>();

        //        foreach (var item in context.ArticuloTipo)
        //        {
        //            typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
        //        }
        //        createArticleViewModel.Types = typeList;

        //        return View(createArticleViewModel);
        //    }
        //}

        //[HttpGet]
        //public IActionResult EditArticle(int articleId, int lotId)
        //{

        //    if (context.Lote.Any(x => x.Id == lotId))
        //    {
        //        var article = context.Articulo.FirstOrDefault(u => u.Id == articleId);

        //        if (article == null)
        //            return RedirectToAction("Index", new { lotId = lotId });
        //        else
        //        {
        //            var model = new EditArticleViewModel
        //            {
        //                TypeId = article.IdTipo,
        //                Brand = article.Marca,
        //                Description = article.Descripcion,
        //                SerialNumber = article.NumeroSerie,
        //                State = article.Estado,
        //                Lot_ID = article.IdLoteNavigation.Id,
        //                ArticleId = articleId,
        //                Price = Convert.ToInt32(Decimal.ToInt32(article.Precio))
        //            };
        //            var typeList = new List<SelectListItem>();

        //            foreach (var item in context.ArticuloTipo)
        //            {
        //                typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
        //            }
        //            model.Types = typeList;

        //            return View(model);
        //        }

        //    }
        //    else
        //        return RedirectToAction("Index", new { lotId = lotId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditArticle(EditArticleViewModel editarArticleViewModel)
        //{
        //    var article = context.Articulo.FirstOrDefault(u => u.Id == editarArticleViewModel.ArticleId);

        //    if (ModelState.IsValid)
        //    {
        //        if (article != null)
        //        {
        //            article.IdTipo = editarArticleViewModel.TypeId;
        //            article.Marca = editarArticleViewModel.Brand;
        //            article.Descripcion = editarArticleViewModel.Description;
        //            article.NumeroSerie = editarArticleViewModel.SerialNumber;
        //            article.Precio = (int)editarArticleViewModel.Price;
        //            context.SaveChanges();
        //        }
        //        return RedirectToAction("Index", new { lotId = article.IdLoteNavigation.Id});
        //    }
        //    else
        //    {
        //        var typeList = new List<SelectListItem>();

        //        foreach (var item in context.ArticuloTipo)
        //        {
        //            typeList.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
        //        }
        //        editarArticleViewModel.Types = typeList;

        //        return View(editarArticleViewModel);
        //    }
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
    
    
        //public IActionResult Index()
        //{
            //return View();
        //}

        //public IActionResult CreateArticle()
        //{
            //return View();
        //}

        //public IActionResult EditArticle()
        //{
            //return View();
        //}

        //public IActionResult IndexPublic()
        //{
            //return View();
        //} 
    }
}
