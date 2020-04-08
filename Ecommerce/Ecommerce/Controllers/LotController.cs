using Ecommerce.Helpers;
using Ecommerce.Tables;
using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.Lot;
using static Ecommerce.Tables.LotHistory;

namespace Ecommerce.Controllers
{
    public class LotController
    {
        private readonly Context context;
        private readonly IStringLocalizer<LotController> localizer;

        public LotController(Context context, IStringLocalizer<LotController> localizer)
        {
            this.context = context;
            this.localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetLots()
        {
            var Lots = context.Lots.Select(l => new
            {
                lot_id = l.LotId,
                lot_Description = l.Description,
                create_Date = l.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                update_Date = l.UpdateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                lot_Articles = l.Articles.Count,
                state = localizer[l.State.GetAttribute<DisplayAttribute>().Name].Value,
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
        public CreateLot(CreateLotViewModel createLotViewModel)
        {
            if (ModelState.IsValid)
            {
                Lot lot = new Lot
                {
                    Description = createLotViewModel.Description,
                    State = Lot.LotState.ENABLED,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };

                if (createLotViewModel.Image != null)
                {
                    lot.ContentLength = createLotViewModel.Image.Length;
                    lot.ContentType = createLotViewModel.Image.ContentType;
                    lot.ImageName = createLotViewModel.Image.FileName;
                    using (var ms = new MemoryStream())
                    {
                        createLotViewModel.Image.CopyTo(ms);
                        lot.Image = ms.ToArray();
                    }
                }

                context.Lots.Add(lot);
                context.SaveChanges();

                NewHistory(lot.LotId, LotAction.CREATE, DateTime.Now, CurrentUserId);

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
            var lot = context.Lots.FirstOrDefault(u => u.LotId == lotId);
            if (lot == null)
                return RedirectToAction("Status", "Error", new { code = 404 });
            else
                return View(new EditLotViewModel
                {
                    Description = lot.Description,
                    LotId = lot.LotId,
                    ImageName = lot.ImageName,
                    FlagImage = true
                });
        }

        [HttpPost]
        public IActionResult EditLot(EditLotViewModel editLotViewModel)
        {
            if (ModelState.IsValid)
            {
                var lot = context.Lots.FirstOrDefault(u => u.LotId == editLotViewModel.LotId);
                if (lot != null)
                {
                    lot.Description = editLotViewModel.Description;
                    lot.UpdateDate = DateTime.Now;

                    if (editLotViewModel.Image != null)
                    {
                        lot.ContentLength = editLotViewModel.Image.Length;
                        lot.ContentType = editLotViewModel.Image.ContentType;
                        lot.ImageName = editLotViewModel.Image.FileName;
                        using (var ms = new MemoryStream())
                        {
                            editLotViewModel.Image.CopyTo(ms);
                            lot.Image = ms.ToArray();
                        }
                    }
                    else
                    if (!editLotViewModel.FlagImage)
                    {
                        lot.ContentLength = 0;
                        lot.ContentType = null;
                        lot.ImageName = null;
                        lot.Image = null;
                    }

                    context.SaveChanges();

                    NewHistory(lot.LotId, LotAction.EDIT, DateTime.Now, CurrentUserId);
                }

                return RedirectToAction("Index");
            }
            else
                return View(editLotViewModel);
        }


    }
}
