using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;

namespace Ecommerce.Core.Managers
{
    public class Lote : ILoteManager
    {
        public ICollection<Common.DataMembers.Output.Lote> Get()
        {
            throw new NotImplementedException();
        }

        public Common.DataMembers.Output.Lote GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Common.DataMembers.Output.Lote Save(Common.DataMembers.Input.Lote lote)
        {
            throw new NotImplementedException();
        }
    }
}
