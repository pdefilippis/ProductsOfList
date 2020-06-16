using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ecommerce.Core.Validations;
using Ecommerce.Common.FaultContracts;

namespace Ecommerce.Core.Managers
{
    public class Lote : ILoteManager
    {
        private readonly ILogger<Lote> _logger;
        private readonly ILoteInfrastructure _loteInfrastructure;
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        private readonly INotificacionesInfrastructure _notificacionesInfrastructure;

        public Lote(ILoteInfrastructure loteInfrastructure, IArticuloInfrastructure articuloInfrastructure,
            IUsuarioInfrastructure usuarioInfrastructure,
            INotificacionesInfrastructure notificacionesInfrastructure)
        {
            _loteInfrastructure = loteInfrastructure;
            _articuloInfrastructure = articuloInfrastructure;
            _usuarioInfrastructure = usuarioInfrastructure;
            _notificacionesInfrastructure = notificacionesInfrastructure;
        }

        public bool Enable(int lote)
        {
            
            var item = _loteInfrastructure.GetById(lote);
            if (!item.Activo)
            {
                _loteInfrastructure.ChangeStatus(lote);
                return true;
            }

            return false;
        }

        public bool Disable(int lote)
        {
            
            var item = _loteInfrastructure.GetById(lote);
            if (item.Activo)
            {
                _loteInfrastructure.ChangeStatus(lote);
                return true;
            }
            return false;
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
            
            var validation = new LoteValidation(_loteInfrastructure);
            var results = validation.Validate(lote);

            if (!results.IsValid)
                throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

            return _loteInfrastructure.Save(lote);
            
        }

        public ICollection<Common.DataMembers.Output.Usuario> Sorteo(int lote)
        {
            
            var ganadores = new List<Common.DataMembers.Output.Usuario>();
            var articulos = _articuloInfrastructure.GetByLote(lote);

            articulos.ToList().ForEach(x => {
                var users = _usuarioInfrastructure.GetByArticulo(x.Id);
                if (!users.Any()) return;

                var ganador = new Random().Next(1, users.Count);

                var usr = users.Skip(ganador - 1).Take(1).FirstOrDefault();
                ganadores.Add(usr);
                _articuloInfrastructure.AdjudicarArticulo(x.Id, usr.Id);
                _notificacionesInfrastructure.Create(new Common.DataMembers.Input.Notificacion { IdArticulo = x.Id, IdUsuario = usr.Id });
            });

            _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Cerrado);

            return ganadores;
            
        }

        public ICollection<Common.DataMembers.Output.Lote> GetAll()
        {
            
            return _loteInfrastructure.GetAll();
            
        }

        public bool Open(int lote)
        {
            _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Abierto);
            return true;
        }

        public bool Close(int lote)
        {
            _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Cerrado);
            return true;
        }

        public ICollection<Common.DataMembers.Output.Lote> GetOpen()
        {
            throw new NotImplementedException();
        }
    }
}
