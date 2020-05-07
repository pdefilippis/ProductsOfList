using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Domain;
using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using Lote = Ecommerce.Common.DataMembers.Input.Lote;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        private readonly Core.ILoteManager _loteManager;
        private readonly IConnectionContext _context;

        public LotController(Core.ILoteManager loteManager, IConnectionContext context)
        {
            _loteManager = loteManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //TODO: Aca tenes la lista de lotes activos. Para poder completar la Grilla.
            var lotes = _loteManager.Get();
            return View();
        }

        public JsonResult GetLots()
        {
            var lotes = _loteManager.GetAll();

            var items = lotes.Select(l => new
            {
                state = l.Activo == true ? "Activo" : "Inactivo",
                lot_id = l.Id,
                lot_Description = l.Descripcion,
                lot_article = l.Articulos.Count,
            }).ToList();

            return Json(items);
        }

        [HttpGet]
        public IActionResult CreateLot()
        {
            return View(new CreateLotViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLot(CreateLotViewModel loteModel)
        {
            if (ModelState.IsValid)
            {
                var lote = new Lote
                {
                    Id = loteModel.LotId,
                    Descripcion = loteModel.Descripcion,
                    NombreImagen = loteModel.Imagen?.FileName,
                    Imagen = ConvertFileToByte(loteModel.Imagen)
                };


                _loteManager.Save(lote);

                return RedirectToAction("Index");
            }
            else
            {
                if (loteModel.Imagen == null)
                    ModelState.AddModelError("Image", "Error al cargar la imagen");

                return View(loteModel);
            }
        }


        [HttpGet]
        public IActionResult EditLot(int LotId)
        {
            var lote = _loteManager.GetById(LotId);

            if (lote == null)
                return RedirectToAction("Status", "Error", new { code = 404 });


            return View(new EditLotViewModel
            {
                Descripcion = lote.Descripcion,
                LotId = lote.Id,
                NombreImagen = lote.NombreImagen,
                FlagImage = lote != null
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLot(EditLotViewModel loteModel)
        {
            if (ModelState.IsValid)
            {
                var lote = new Lote
                {
                    Id = loteModel.LotId,
                    Descripcion = loteModel.Descripcion,
                    NombreImagen = loteModel.Imagen?.FileName,
                    Imagen = ConvertFileToByte(loteModel.Imagen)
                };


                _loteManager.Save(lote);

                return RedirectToAction("Index");
            }
            
            return View(loteModel);
        }
             
        public IActionResult EnableDisable(int LotId)
        {
            using (var context = _context.Get())
            {
                var article = context.Lote.FirstOrDefault(u => u.Id == LotId);
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

            return RedirectToAction("Index");
        }
        
        public IActionResult History()
        {
            return View();
        }

        private byte[] ConvertFileToByte(IFormFile image)
        {
            byte[] img = null;

            if (image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    img = ms.ToArray();
                }
            }

            return img;
        }
    }
}
