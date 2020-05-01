using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;

namespace Ecommerce.Core.Managers
{
    public class Lote : ILoteManager
    {
        private readonly ILoteInfrastructure _loteInfrastructure;
        public Lote(ILoteInfrastructure loteInfrastructure)
        {
            _loteInfrastructure = loteInfrastructure;
        }
        public ICollection<Common.DataMembers.Output.Lote> Get()
        {
            return _loteInfrastructure.Get();
        }

        public Common.DataMembers.Output.Lote GetById(int id)
        {
            return _loteInfrastructure.GetById(id);
        }

        public Common.DataMembers.Output.Lote Save(Common.DataMembers.Input.Lote lote)
        {
            return _loteInfrastructure.Save(lote);
        }
    }
}
