using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;

namespace Ecommerce.Infrastructure.Repository
{
    public class ArticuloTipo : IArticuloTipoInfrastructure
    {
        public ICollection<Common.DataMembers.Output.ArticuloTipo> Get()
        {
            throw new NotImplementedException();
        }

        public Common.DataMembers.Output.ArticuloTipo GetByCodigo(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
