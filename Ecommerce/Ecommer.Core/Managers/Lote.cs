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
        public Lote(ILoteInfrastructure loteInfrastructure, IArticuloInfrastructure articuloInfrastructure,
            IUsuarioInfrastructure usuarioInfrastructure, ILogger<Lote> logger)
        {
            _loteInfrastructure = loteInfrastructure;
            _articuloInfrastructure = articuloInfrastructure;
            _usuarioInfrastructure = usuarioInfrastructure;
            _logger = logger;
        }

        public void Enable(int lote)
        {
            try
            {
                var item = _loteInfrastructure.GetById(lote);
                if (!item.Activo)
                    _loteInfrastructure.ChangeStatus(lote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public void Disable(int lote)
        {
            try
            {
                var item = _loteInfrastructure.GetById(lote);
                if (item.Activo)
                    _loteInfrastructure.ChangeStatus(lote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public ICollection<Common.DataMembers.Output.Lote> Get()
        {
            try
            {
                return _loteInfrastructure.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }            
        }

        public Common.DataMembers.Output.Lote GetById(int id)
        {
            try
            {
                return _loteInfrastructure.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.Lote Save(Common.DataMembers.Input.Lote lote)
        {
            try
            {
                var validation = new LoteValidation(_loteInfrastructure);
                var results = validation.Validate(lote);

                //if (!results.IsValid)
                //    throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

                return _loteInfrastructure.Save(lote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public void Sorteo(int lote)
        {
            try
            {
                var articulos = _articuloInfrastructure.GetByLote(lote);

                articulos.ToList().ForEach(x => {
                    var users = _usuarioInfrastructure.GetByArticulo(x.Id);
                    if (!users.Any()) return;

                    var ganador = new Random().Next(1, users.Count);

                    var usr = users.Skip(ganador - 1).Take(1).FirstOrDefault();

                    _articuloInfrastructure.AdjudicarArticulo(x.Id, usr.Id);
                });

                _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Cerrado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public ICollection<Common.DataMembers.Output.Lote> GetAll()
        {
            try
            {
                return _loteInfrastructure.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public void Open(int lote)
        {
            _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Abierto);
        }

        public void Close(int lote)
        {
            _loteInfrastructure.ChangeStatus(lote, Ecommerce.Common.Constant.Properties.Estado.Cerrado);
        }
    }
}
