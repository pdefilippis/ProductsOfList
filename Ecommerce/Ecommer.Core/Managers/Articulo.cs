using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        public ICollection<Common.DataMembers.Output.Articulo> Get()
        {
            throw new NotImplementedException();
        }

        public Common.DataMembers.Output.Articulo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetLote(int lote)
        {
            throw new NotImplementedException();
        }

        public Common.DataMembers.Output.Articulo Save(Common.DataMembers.Input.Articulo articulo)
        {
            throw new NotImplementedException();
        }
    }
}
