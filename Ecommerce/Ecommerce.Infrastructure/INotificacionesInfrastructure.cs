using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Infrastructure
{
    public interface INotificacionesInfrastructure
    {
        ICollection<Member.Output.Notificacion> GetByUser(int idUsuario);
        ICollection<Member.Output.Notificacion> GetByUser(string userName);
        void RecordReading(int idUsuario);
        void RecordReading(string userName);
        void Create(Member.Input.Notificacion notificacion);
    }
}
