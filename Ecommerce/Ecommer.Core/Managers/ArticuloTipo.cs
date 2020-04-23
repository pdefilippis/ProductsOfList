using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;

namespace Ecommerce.Core.Managers
{
    public class ArticuloTipo : IArticuloTipoManager
    {
        private readonly IArticuloTipoInfrastructure _articuloTipoInfrastructure;
        public ArticuloTipo(IArticuloTipoInfrastructure articuloTipoInfrastructure)
        {
            _articuloTipoInfrastructure = articuloTipoInfrastructure;
        }

        public ICollection<Common.DataMembers.Output.ArticuloTipo> Get()
        {
            return _articuloTipoInfrastructure.Get();
        }

        public Common.DataMembers.Output.ArticuloTipo GetByCodigo(string codigo)
        {
            return _articuloTipoInfrastructure.GetByCodigo(codigo);
        }

        public Common.DataMembers.Output.ArticuloTipo GetById(int id)
        {
            return _articuloTipoInfrastructure.GetById(id);
        }
    }
}
