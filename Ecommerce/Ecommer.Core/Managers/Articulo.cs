using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        private readonly ILogger<Articulo> _logger;
        public Articulo(IArticuloInfrastructure articuloInfrastructure, ILogger<Articulo> logger)
        {
            _articuloInfrastructure = articuloInfrastructure;
            _logger = logger;
        }

        public void DeclinarPostulacionArticulo(ArticuloPostulacion postulacion)
        {
            try
            {
                if (_articuloInfrastructure.ExistsPostulacion(postulacion))
                    _articuloInfrastructure.DeclinarPostulacion(postulacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public void Disable(int articulo)
        {
            try
            {
                var item = _articuloInfrastructure.GetById(articulo);
                if (item.Activo)
                    _articuloInfrastructure.ChangeStatus(articulo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public void Enable(int articulo)
        {
            try
            {
                var item = _articuloInfrastructure.GetById(articulo);
                if (!item.Activo)
                    _articuloInfrastructure.ChangeStatus(articulo);
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
            try
            {
                return _articuloInfrastructure.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
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

        public void PostularArticulo(ArticuloPostulacion postulacion)
        {
            try
            {
                if (!_articuloInfrastructure.ExistsPostulacion(postulacion))
                    _articuloInfrastructure.Postular(postulacion);
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
