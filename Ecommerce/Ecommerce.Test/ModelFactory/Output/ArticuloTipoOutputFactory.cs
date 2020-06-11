using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Test.ModelFactory.Domain;

namespace Ecommerce.Test.ModelFactory.Output
{
    public static class ArticuloTipoOutputFactory
    {
        private static bool load = true;
        public static ArticuloTipo Get()
        {
            //if (!load) return null;
            load = !load;

            var item = ArticuloTipoFactory.Get();

            return new ArticuloTipo
            {
                Codigo = item.Codigo,
                Descripcion = item.Descripcion,
                Id = item.Id
            };
        }

        public static List<ArticuloTipo> GetList()
        {
            return new List<ArticuloTipo> { Get() };
        }
    }
}
