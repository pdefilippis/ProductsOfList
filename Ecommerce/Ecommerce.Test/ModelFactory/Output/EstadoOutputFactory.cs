using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Test.ModelFactory.Domain;

namespace Ecommerce.Test.ModelFactory.Output
{
    public static class EstadoOutputFactory
    {
        private static bool load = true;
        public static Estado Get()
        {
            //if (!load) return null;
            load = !load;

            var item = EstadoFactory.Get();

            return new Estado
            {
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }
    }
}
