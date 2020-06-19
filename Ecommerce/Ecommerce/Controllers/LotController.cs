using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Input = Ecommerce.Common.DataMembers.Input;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        private readonly Core.ILoteManager _loteManager;
        private readonly ILogger<LotController> _logger;
        private readonly Core.IArticuloManager _articuloManager;

        public LotController(Core.ILoteManager loteManager, Core.IArticuloManager articuloManager, ILogger<LotController> logger)
        {
            _loteManager = loteManager;
            _logger = logger;
            _articuloManager = articuloManager;
        }

        public IActionResult Index()
        {
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
                create_Date = l.Creacion.ToString("dd/MM/yyyy"),
                update_Date = l.Actualizacion.ToString("dd/MM/yyyy"),
                lot_article = l.Articulos.Count,
                cerrado = l.Estado.Codigo.Equals("CERRADO") ? true : false
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
            try
            {
                var descriptionValidator = _loteManager.GetAll();
                if (loteModel.Descripcion == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                }
                else
                {
                    if (descriptionValidator.Any(l => l.Descripcion.ToLower() == loteModel.Descripcion.ToLower()))
                        ModelState.AddModelError("", "Ya existe un lote con esta descripcion");
                }


                if (ModelState.IsValid)
                {
                    var lote = new Input.Lote
                    {
                        Id = loteModel.LotId,
                        Descripcion = loteModel.Descripcion,
                        NombreImagen = loteModel.Imagen?.FileName,
                        Imagen = ConvertFileToByte(loteModel.Imagen)
                    };

                    TempData["SuccesMessage"] = "Se ha creado un lote";

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
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }


        [HttpGet]
        public IActionResult EditLot(int LotId)
        {
            var lote = _loteManager.GetById(LotId);

            try
            {
                if (lote == null)
                    return RedirectToAction("Status", "Error", new { code = 404 });


                return View(new EditLotViewModel
                {
                    Descripcion = lote.Descripcion,
                    LotId = lote.Id,
                    NombreImagen = lote.NombreImagen,
                    FlagImage = lote != null,
                    ImagenEdit = lote.Imagen
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLot(EditLotViewModel loteModel)
        {
            try
            {
                var descriptionValidator = _loteManager.GetAll();
                if (loteModel.Descripcion == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                }
                else
                {
                    if (descriptionValidator.Any(l => l.Id != loteModel.LotId && l.Descripcion.ToLower() == loteModel.Descripcion.ToLower()))
                        ModelState.AddModelError("", "Ya existe un lote con esta descripcion");
                }


                if (ModelState.IsValid)
                {
                    var lotePrev = _loteManager.GetById(loteModel.LotId);
                    var lote = new Input.Lote
                    {
                        Id = loteModel.LotId,
                        Descripcion = loteModel.Descripcion,
                        NombreImagen = lotePrev.NombreImagen,
                        Imagen = lotePrev.Imagen
                    };

                    if (loteModel.Imagen != null)
                    {
                        lote.NombreImagen = loteModel.Imagen.FileName;
                        lote.Imagen = ConvertFileToByte(loteModel.Imagen);
                    }
                    else
                   if (!loteModel.FlagImage)
                    {
                        lote.NombreImagen = null;
                        lote.Imagen = null;
                    }



                    TempData["SuccesMessage"] = "El lote se ha editado correctamente";

                    _loteManager.Save(lote);

                    return RedirectToAction("Index");
                }

                return View(loteModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        //TODO: Modificar por dos acciones diferentes
        public IActionResult EnableDisable(int LotId)
        {
            try
            {
                var lote = _loteManager.GetById(LotId);
                if (lote.Activo)
                {
                    TempData["ErrorMessage"] = "El lote se ha desactivado";
                    _loteManager.Disable(LotId);
                }
                else
                {
                    TempData["SuccesMessage"] = "El lote se ha activado";
                    _loteManager.Enable(LotId);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public JsonResult LotClosure(int LotId)
        {
            var item = _loteManager.Get();
            var lot = item.FirstOrDefault(l => l.Id == LotId);

            if (lot != null)
            {
                _loteManager.Sorteo(LotId);
                _loteManager.Close(LotId);

                return Json(true);
            }
            return Json(false);
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
