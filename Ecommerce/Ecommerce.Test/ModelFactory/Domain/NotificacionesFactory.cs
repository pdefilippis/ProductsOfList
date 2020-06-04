using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class NotificacionesFactory
    {
        private static int load = 0;
        public static Notificaciones Get()
        {
            //if (load > 3) return null;
            load++;

            return new Notificaciones
            {
                Id = 81,
                IdArticulo = ArticuloFactory.Get().Id,
                //IdArticuloNavigation = ArticuloFactory.Get(),
                IdUsuario = 12,
                //IdUsuarioNavigation = UsuarioFactory.Get(),
                Leido = false,
                Stamp = new DateTime(2020, 5, 2, 17, 43, 21)
            };
        }

        public static List<Notificaciones> GetList()
        {
            return new List<Notificaciones>
            {
                Get()
            };
        }
    }
}
