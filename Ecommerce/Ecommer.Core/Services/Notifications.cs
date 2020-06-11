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
    }
}
