using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class ArticuloTipoFactory
    {
        private static int load = 0;

        public static ArticuloTipo Get()
        {
            //if (load > 3) return null;
            load++;

            return new ArticuloTipo
            {
                Activo = true,
                //Articulo = ArticuloFactory.GetList(),
                Codigo = "MONT",
                Descripcion = "Monitores",
                Id = 821
            };
        }
    }
}
