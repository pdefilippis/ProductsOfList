using Ecommerce.Common.DataMembers.Input;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ecommerce.Core.Services
{
    public class Notifications
    {
        public void SendRecoverPassword(string to, string token)
        {
            var gestor = new GestorCorreo();
            gestor.EnviarCorreo(to, "Recuperar Clave", string.Format("Ingrese el siguiente codigo: <b>{0}</b>", token), true);
        }

        public void Contactenos(string nombre, string email, string asunto, string mensaje)
        {
            var gestor = new GestorCorreo();

            var cuerpo = new StringBuilder();

            cuerpo.AppendLine(string.Format("Nombre: {0}", nombre));
            cuerpo.AppendLine(string.Format("Email: {0}", email));
            cuerpo.AppendLine(string.Format("Asunto: {0}", asunto));
            cuerpo.AppendLine(string.Format("Mensaje: {0}", mensaje));

            gestor.EnviarCorreo("soporteremate@gmail.com", "Contacto Usuario", cuerpo.ToString(), true);
        }
    }
}
