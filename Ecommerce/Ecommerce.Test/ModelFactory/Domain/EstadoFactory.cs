using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class EstadoFactory
    {
        private static int load = 0;
        public static Estado Get()
        {
            //if (load > 3) return null;
            load++;

            return new Estado
            {
                Activo = true,
                Codigo = "OPEN",
                Descripcion = "Descripcion",
                Id = 52,
                //Lote = LoteFactory.GetList()
            };
        }
    }
}
