using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class SolicitudFactory
    {
        private static int load = 0;
        public static Solicitud Get()
        {
            //if (load > 3) return null;
            load++;

            return new Solicitud
            {
                Id = 12,
                IdArticulo = ArticuloFactory.Get().Id,
                //IdArticuloNavigation = ArticuloFactory.Get(),
                IdUsuario = UsuarioFactory.Get().Id,
                //IdUsuarioNavigation = UsuarioFactory.Get()
            };
        }

        public static List<Solicitud> GetList()
        {
            return new List<Solicitud>
            {
                Get()
            };
        }
    }
}
