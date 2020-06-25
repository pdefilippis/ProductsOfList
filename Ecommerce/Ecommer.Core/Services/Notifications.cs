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

            cuerpo.AppendLine(string.Format("Nombre: <b>{0}</b><br>", nombre));
            cuerpo.AppendLine(string.Format("Email: <b>{0}</b><br>", email));
            cuerpo.AppendLine(string.Format("Asunto: <b>{0}</b><br>", asunto));
            cuerpo.AppendLine(string.Format("Mensaje: <b>{0}", mensaje));

            gestor.EnviarCorreo("remate.contacto.soporte@gmail.com", "Contacto Usuario", cuerpo.ToString(), true);
        }
    }
}
