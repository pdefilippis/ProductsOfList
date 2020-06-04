using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class LoteFactory
    {
        private static int load = 0;
        public static Lote Get()
        {
            //if (load > 3) return null;
            load++;

            return new Lote
            {
                Activo = true,
                Actualizacion = new DateTime(2020, 5, 7, 20, 17, 57),
                //Articulo = ArticuloFactory.GetList(),
                Creacion = new DateTime(2020, 2, 9, 15, 32, 2),
                Descripcion = "Monitores",
                Id = 32,
                IdEstado = 43,
                IdEstadoNavigation = EstadoFactory.Get(),
                //Imagen = new byte[] { },
                NombreImagen = "Lote.jpg"
            };
        }

        public static List<Lote> GetList()
        {
            return new List<Lote> { Get() };
        }
    }
}
