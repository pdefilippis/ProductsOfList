using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Test.ModelFactory.Domain;

namespace Ecommerce.Test.ModelFactory.Output
{
    public static class ArticuloOutputFactory
    {
        private static bool load = true;
        public static Articulo Get()
        {
            //if (!load) return null;
            load = !load;

            var item = ArticuloFactory.Get();

            return new Articulo
            {
                Activo = item.Activo,
                Descripcion = item.Descripcion,
                Id = item.Id,
                Lote = LoteOutputFactory.Get(),
                Marca = item.Marca,
                NumeroSerie = item.NumeroSerie,
                Precio = item.Precio,
                Tipo = ArticuloTipoOutputFactory.Get(),
                UsuarioAdjudicado = UsuarioOutputFactory.Get(),
                UsuariosInteresados = UsuarioOutputFactory.GetList()
            };
        }

        public static List<Articulo> GetList()
        {
            return new List<Articulo>
            {
                Get()
            };
        }
    }
}
