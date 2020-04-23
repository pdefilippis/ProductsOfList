using Ecommerce.Domain.Models;
using Ecommerce.Helpers;
using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using static Ecommerce.Domain.Models.Lote;
using static Ecommerce.Domain.Models.LoteHistorial;

namespace Ecommerce.Controllers
{
    public class LotController : _BaseController
    {
        private readonly ProductsManagerContext context;
        private readonly IStringLocalizer<LotController> localizer;
        static Random random = new Random();

        public LotController(ProductsManagerContext productsManagerContext, IStringLocalizer<LotController> localizer)
        {
            this.context = productsManagerContext;
            this.localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetLots()
        {
            //Agarro los valores del lote y los mapeo
            var Lots = context.Lote.Select(l => new
            {
                lot_id = l.Id,
                lot_Description = l.Descripcion,
                create_Date = l.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                update_Date = l.UpdateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                lot_Articles = l.Articulo.Count,
                state = localizer[l.Estado.GetAttribute<DisplayAttribute>().Name].Value,
            }).ToList();
            return Json(Lots);
        }


        [HttpGet]
        public IActionResult CreateLot()
        {
            var model = new CreateLotViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLot(CreateLotViewModel createLotViewModel)
        {
            if (ModelState.IsValid)
            {
                Lote lot = new Lote
                {
                    Descripcion = createLotViewModel.Description,
                    Estado = Lote.EstadoLote.ENABLED,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };

                if (createLotViewModel.Image != null)
                {
                    lot.ContentLength = createLotViewModel.Image.Length;
                    lot.ContentType = createLotViewModel.Image.ContentType;
                    lot.NombreImagen = createLotViewModel.Image.FileName;
                    using (var ms = new MemoryStream())
                    {
                        createLotViewModel.Image.CopyTo(ms);
                        lot.Imagen = ms.ToArray();
                    }
                }

                context.Lote.Add(lot);
                context.SaveChanges();

                NewHistory(lot.Id, AccionLote.CREATE, DateTime.Now, CurrentUserId);

                return RedirectToAction("Index");
            }
            else
            {
                if (createLotViewModel.Image == null)
                    ModelState.AddModelError("Image", "Error al cargar la imagen");

                return View(createLotViewModel);
            }
        }


        [HttpGet]
        public IActionResult EditLot(int lotId)
        {
            var lot = context.Lote.FirstOrDefault(u => u.Id == lotId);
            if (lot == null)
                return RedirectToAction("Status", "Error", new { code = 404 });
            else
                return View(new EditLotViewModel
                {
                    Description = lot.Descripcion,
                    LotId = lot.Id,
                    ImageName = lot.NombreImagen,
                    FlagImage = true
                });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLot(EditLotViewModel editLotViewModel)
        {
            if (ModelState.IsValid)
            {
                var lot = context.Lote.FirstOrDefault(u => u.Id == editLotViewModel.LotId);
                if (lot != null)
                {
                    lot.Descripcion = editLotViewModel.Description;
                    lot.UpdateDate = DateTime.Now;

                    if (editLotViewModel.Image != null)
                    {
                        lot.ContentLength = editLotViewModel.Image.Length;
                        lot.ContentType = editLotViewModel.Image.ContentType;
                        lot.NombreImagen = editLotViewModel.Image.FileName;
                        using (var ms = new MemoryStream())
                        {
                            editLotViewModel.Image.CopyTo(ms);
                            lot.Imagen = ms.ToArray();
                        }
                    }
                    else
                    if (!editLotViewModel.FlagImage)
                    {
                        lot.ContentLength = 0;
                        lot.ContentType = null;
                        lot.NombreImagen = null;
                        lot.Imagen = null;
                    }

                    context.SaveChanges();

                    NewHistory(lot.Id, AccionLote.EDIT, DateTime.Now, CurrentUserId);
                }

                return RedirectToAction("Index");
            }
            else
                return View(editLotViewModel);
        }

        public IActionResult EnableDisable(int lotId)
        {
            var lot = context.Lote.FirstOrDefault(u => u.Id == lotId);

            if (lot == null)
                return RedirectToAction("Index");
            else
            {
                switch (lot.Estado)
                {
                    case EstadoLote.DISABLED:
                        lot.Estado = EstadoLote.ENABLED;
                        NewHistory(lot.Id, AccionLote.ENABLE, DateTime.Now, CurrentUserId);
                        break;

                    case EstadoLote.ENABLED:
                        lot.Estado = EstadoLote.DISABLED;
                        NewHistory(lot.Id, AccionLote.DISABLE, DateTime.Now, CurrentUserId);
                        break;
                }
            }
            context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult History(int lotId)
        {

            if (context.Lote.Any(x => x.Id == lotId))
            {
                HistoryLotViewModel historyLotViewModel = new HistoryLotViewModel();
                historyLotViewModel.LotId = lotId;

                return View(historyLotViewModel);
            }
            else
                return RedirectToAction("Status", "Error", new { code = 404 });

        }

        public JsonResult GetHistory(int lotId)
        {

            var lotHistory = context.LoteHistoriales.Where(x => x.Lote.Id == lotId).Select(x => new
            {
                description = x.Lote.Descripcion,
                user = x.Usuario.Usuario1,
                action = localizer[x.Action.GetAttribute<DisplayAttribute>().Name].Value,
                date = x.Date.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            return Json(lotHistory);
        }

        public JsonResult LotClosure(int lotId)
        {
            var lot = context.Lote.FirstOrDefault(l => l.Id == lotId);

            if (lot != null)
            {
                foreach (var article in lot.Articulo)
                {
                    if (article.UserArticles.Count > 0)
                    {
                        int r = random.Next(article.UserArticles.Count);
                        article.IdUsuarioAdjudicado = article.UserArticles.ElementAt(r).UserId;
                    }
                }

                lot.Estado = EstadoLote.CLOSED;

                context.SaveChanges();

                NewHistory(lot.Id, AccionLote.CLOSE, DateTime.Now, CurrentUserId);

                return Json(true);
            }
            return Json(false);
        }

        public void NewHistory(int lotId, AccionLote lotAction, DateTime dateAction, int userId)
        {

            LoteHistorial lotHistory = new LoteHistorial
            {
                LotId = lotId,
                Action = lotAction,
                Date = dateAction,
                UserId = userId
            };

            context.LoteHistoriales.Add(lotHistory);
            context.SaveChanges();
        }


        //public IActionResult CreateLot()
        //{
        //    return View();
        //}

        //public IActionResult EditLot()
        //{
        //    return View();
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult History()
        //{
        //    return View();
        //}
    }
}
