using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;


namespace Ecommerce.Core
{
    public interface INotificacionesManager
    {
        ICollection<Member.Output.Notificacion> GetByUser(string userName);
        void RecordReading(string userName);
        void SendRequestConnected(string nombre, string email, string asunto, string mensaje);
    }
}
