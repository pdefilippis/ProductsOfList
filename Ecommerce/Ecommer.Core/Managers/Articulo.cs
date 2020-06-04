using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Common.FaultContracts;
using Ecommerce.Core.Validations;
using Ecommerce.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        private readonly ILogger<Articulo> _logger;
        public Articulo(IArticuloInfrastructure articuloInfrastructure/*, ILogger<Articulo> logger*/)
        {
            _articuloInfrastructure = articuloInfrastructure;
            //_logger = logger;
        }

        public bool DeclinarPostulacionArticulo(ArticuloPostulacion postulacion)
        {
            try
            {
                if (_articuloInfrastructure.ExistsPostulacion(postulacion))
                {
                    _articuloInfrastructure.DeclinarPostulacion(postulacion);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public bool Disable(int articulo)
        {
            try
            {
                var item = _articuloInfrastructure.GetById(articulo);
                if (item.Activo)
                {
                    _articuloInfrastructure.ChangeStatus(articulo);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public bool Enable(int articulo)
        {
            try
            {
                var item = _articuloInfrastructure.GetById(articulo);
                if (!item.Activo)
                {
                    _articuloInfrastructure.ChangeStatus(articulo);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }   
        }

        public ICollection<Common.DataMembers.Output.Articulo> Get()
        {
            try
            {
                return _articuloInfrastructure.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetAll()
        {
            try
            {
                return _articuloInfrastructure.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.Articulo GetById(int id)
        {
            return _articuloInfrastructure.GetById(id);
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetLote(int lote)
        {
            try
            {
                return _articuloInfrastructure.GetByLote(lote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public bool PostularArticulo(ArticuloPostulacion postulacion)
        {
            try
            {
                if (!_articuloInfrastructure.ExistsPostulacion(postulacion))
                {
                    _articuloInfrastructure.Postular(postulacion);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.Articulo Save(Common.DataMembers.Input.Articulo articulo)
        {
            try
            {
                var validation = new ArticuloValidation(_articuloInfrastructure);
                var results = validation.Validate(articulo);

                //if (!results.IsValid)
                //    throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

                return _articuloInfrastructure.Save(articulo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }
    }
}
