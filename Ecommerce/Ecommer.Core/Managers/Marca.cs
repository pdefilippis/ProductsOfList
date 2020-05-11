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
    public class Marca : IMarcaManager
    {
        private readonly ILogger<Marca> _logger;
        private readonly IMarcaInfrastructure _marcaInfrastructure;

        public Marca(IMarcaInfrastructure marcaInfrastructure, ILogger<Marca> logger)
        {
            _marcaInfrastructure = marcaInfrastructure;
            _logger = logger;
        }

        public ICollection<Common.DataMembers.Output.Marca> Get()
        {
            try
            {
                return _marcaInfrastructure.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }
    }
}
