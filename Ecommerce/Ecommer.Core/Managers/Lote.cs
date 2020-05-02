using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;
using System.Linq;

namespace Ecommerce.Core.Managers
{
    public class Lote : ILoteManager
    {
        private readonly ILoteInfrastructure _loteInfrastructure;
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        public Lote(ILoteInfrastructure loteInfrastructure, IArticuloInfrastructure articuloInfrastructure,
            IUsuarioInfrastructure usuarioInfrastructure)
        {
            _loteInfrastructure = loteInfrastructure;
            _articuloInfrastructure = articuloInfrastructure;
            _usuarioInfrastructure = usuarioInfrastructure;
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

        public void Sorteo(int lote)
        {
            var articulos = _articuloInfrastructure.GetByLote(lote);

            articulos.ToList().ForEach(x => {
                var users = _usuarioInfrastructure.GetByArticulo(x.Id);
                if (!users.Any()) return;

                var ganador = new Random().Next(1, users.Count);

                var usr = users.Skip(ganador - 1).Take(1).FirstOrDefault();

                _articuloInfrastructure.AdjudicarArticulo(x.Id, usr.Id);
            });
        }
    }
}
