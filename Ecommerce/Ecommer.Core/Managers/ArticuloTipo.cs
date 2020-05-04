using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.Managers
{
    public class ArticuloTipo : IArticuloTipoManager
    {
        private readonly IArticuloTipoInfrastructure _articuloTipoInfrastructure;
        private readonly ILogger<ArticuloTipo> _logger;
        public ArticuloTipo(IArticuloTipoInfrastructure articuloTipoInfrastructure, ILogger<ArticuloTipo> logger)
        {
            _articuloTipoInfrastructure = articuloTipoInfrastructure;
            _logger = logger;
        }

        public ICollection<Common.DataMembers.Output.ArticuloTipo> Get()
        {
            try
            {
                return _articuloTipoInfrastructure.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.ArticuloTipo GetByCodigo(string codigo)
        {
            try
            {
                return _articuloTipoInfrastructure.GetByCodigo(codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.ArticuloTipo GetById(int id)
        {
            try
            {
                return _articuloTipoInfrastructure.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }
    }
}
