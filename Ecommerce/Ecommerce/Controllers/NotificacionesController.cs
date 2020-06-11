using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly Core.INotificacionesManager _notificacionesManager;
        public NotificacionesController(Core.INotificacionesManager notificacionesManager)
        {
            _notificacionesManager = notificacionesManager;
        }

        public JsonResult ReadNotifications()
        {
            _notificacionesManager.RecordReading(User.Identity.Name);
            return Json(true); 
        }
    }
}