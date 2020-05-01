using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        public Articulo(IArticuloInfrastructure articuloInfrastructure)
        {
            _articuloInfrastructure = articuloInfrastructure;
        }

        public ICollection<Common.DataMembers.Output.Articulo> Get()
        {
            return _articuloInfrastructure.Get();
        }

        public Common.DataMembers.Output.Articulo GetById(int id)
        {
            return _articuloInfrastructure.GetById(id);
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetLote(int lote)
        {
            return _articuloInfrastructure.GetByLote(lote);
        }

        public Common.DataMembers.Output.Articulo Save(Common.DataMembers.Input.Articulo articulo)
        {
            return _articuloInfrastructure.Save(articulo);
        }
    }
}
