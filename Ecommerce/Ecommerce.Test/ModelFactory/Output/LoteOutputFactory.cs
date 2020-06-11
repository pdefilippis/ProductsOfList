using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Test.ModelFactory.Domain;

namespace Ecommerce.Test.ModelFactory.Output
{
    public static class LoteOutputFactory
    {
        private static bool load = true;
        public static Lote Get()
        {
            //if (!load) return null;
            load = !load;

            var item = LoteFactory.Get();

            return new Lote
            {
                Activo = item.Activo,
                Actualizacion = item.Actualizacion,
                //Articulos = ArticuloOutputFactory.GetList(),
                Creacion = item.Creacion,
                Descripcion = item.Descripcion,
                Estado = EstadoOutputFactory.Get(),
                Id = item.Id,
                Imagen = item.Imagen,
                NombreImagen = item.NombreImagen
            };
        }

        public static List<Lote> GetList()
        {
            return new List<Lote> { Get() };
        }
    }
}
