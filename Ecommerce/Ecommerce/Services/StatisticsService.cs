using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class StatisticsService
    {
        private readonly Core.INotificacionesManager _notificacionesManager;
        public StatisticsService(Core.INotificacionesManager notificacionesManager)
        {
            _notificacionesManager = notificacionesManager;
        }

        public int GetCountNotificaciones(string userName)
        {
            return _notificacionesManager.GetByUser(userName).Count();
        }

        public List<Ecommerce.Common.DataMembers.Output.Notificacion> GetNotificaciones(string userName)
        {
            return _notificacionesManager.GetByUser(userName).ToList();
        }
    }

}
