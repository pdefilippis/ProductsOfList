using System;
using System.Collections.Generic;
using System.Text;
using DataMembers = Ecommerce.Common.DataMembers;
using Ecommerce.Infrastructure;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ecommerce.Core.Validations;
using Ecommerce.Common.FaultContracts;


namespace Ecommerce.Core.Managers
{
    public class Notificaciones : INotificacionesManager
    {
        private readonly INotificacionesInfrastructure _notificacionesInfrastructure;
        public Notificaciones(INotificacionesInfrastructure notificacionesInfrastructure)
        {
            _notificacionesInfrastructure = notificacionesInfrastructure;
        }

        public ICollection<DataMembers.Output.Notificacion> GetByUser(string userName)
        {
            return _notificacionesInfrastructure.GetByUser(userName);
        }

        public void RecordReading(string userName)
        {
            _notificacionesInfrastructure.RecordReading(userName);
        }
    }
}
