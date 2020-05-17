using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Domain;
using Ecommerce.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly Core.IUsuarioManager _usuarioManager;

        public ArticleController(Core.ILoteManager loteManager, Core.IUsuarioManager usuarioManager,Core.IArticuloManager articuloManager, Core.IArticuloTipoManager articuloTipoManager, IConnectionContext context)
        {
            _articuloManager = articuloManager;
            _articuloTipoManager = articuloTipoManager;
            _loteManager = loteManager;
            _context = context;
            _usuarioManager = usuarioManager;
        }

        public IActionResult Index(int LotId)
        {
            var lote = _loteManager.GetById(LotId);

            if (lote != null)
            {
                return View(new IndexPublicViewModel()
                {
                    Description = lote.Descripcion,
                    LotId = lote.Id                    
                });
            }
            else
                return RedirectToAction("Status", "Error", new { code = 404 });
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

        public JsonResult GetArticlesPublic(int LotId)
        {
            var article = _articuloManager.GetLote(LotId);
            
            var articles = article.Select(l => new
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


        public IActionResult IndexPublic(int LotId)
        {

            var CurrentUserName = HttpContext.User.Identity.Name;

            var lot = _loteManager.GetById(LotId);

            var token = lot.Articulos.SelectMany(a => a.UsuariosInteresados).Where(u => u.UserName == CurrentUserName).FirstOrDefault();

            var asd = lot.Articulos.SelectMany(a => a.UsuariosInteresados);

            if (lot != null)
            {
                IndexPublicViewModel indexPublicViewModel = new IndexPublicViewModel
                {
                    Description = lot.Descripcion,
                    TakenId = token != null ? token.Id : 0,
                    LotId = LotId
                };

                return View(indexPublicViewModel);
            }

            return RedirectToAction("Status", "Error", new { code = 404 });
        }

        public JsonResult ApplyUnApply(int ArticleId)
        {
            var CurrentUserId = HttpContext.User.Identity.Name;

            var user = _usuarioManager.GetByName(CurrentUserId);

            var lot = _articuloManager.GetById(ArticleId).Lote;

            var token = lot.Articulos.SelectMany(a => a.UsuariosInteresados).Any(u => u.UserName == CurrentUserId);

            
                if (token == false)
                {
                    _articuloManager.PostularArticulo(new ArticuloPostulacion
                    {
                        IdArticulo = ArticleId,
                        IdUsuario = user.Id
                    });
                }
                else
                {
                    _articuloManager.DeclinarPostulacionArticulo(new ArticuloPostulacion
                    {
                        IdArticulo = ArticleId,
                        IdUsuario = user.Id
                    });
                }
            

            return Json(true);
        }

    }
}
