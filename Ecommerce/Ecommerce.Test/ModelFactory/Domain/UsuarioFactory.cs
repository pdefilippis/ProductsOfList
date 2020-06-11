using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class UsuarioFactory
    {
        private static int load = 0;
        public static Usuario Get()
        {
            //if (load > 3) return null;
            load++;

            return new Usuario
            {
                Activo = true,
                Administrador = false,
                Apellido = "Martinez",
                //Articulo = ArticuloFactory.GetList(),
                Clave = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                Creacion = new DateTime(2020, 2, 1, 23, 31, 24),
                Id = 91,
                Mail = "ricardo@gmail.com",
                Nombre = "Ricardo",
                //Notificaciones = NotificacionesFactory.GetList(),
                //Solicitud = SolicitudFactory.GetList(),
                UltimoIngreso = new DateTime(2020, 5, 15, 5, 12, 53),
                Usuario1 = "rm"
            };
        }

        public static List<Usuario> GetList()
        {
            return new List<Usuario>
            {
                Get()
            };
        }
    }
}
