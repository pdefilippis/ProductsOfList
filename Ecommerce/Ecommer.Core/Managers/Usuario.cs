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
    public class Usuario : IUsuarioManager
    {
        private readonly ILogger<Usuario> _logger;
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        public Usuario(IUsuarioInfrastructure usuarioInfrastructure, ILogger<Usuario> logger)
        {
            _usuarioInfrastructure = usuarioInfrastructure;
            _logger = logger;
        }

        public Common.DataMembers.Output.Usuario ChangePassword(Common.DataMembers.Input.ChangePassword usuario)
        {
            try
            {
                var usr = _usuarioInfrastructure.Get(usuario.UserName);
                if (usr.Password == usuario.OldPassword)
                    return _usuarioInfrastructure.ChangePassword(usuario.UserName, usuario.NewPassword);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
            
        }

        public Common.DataMembers.Output.Usuario Login(Common.DataMembers.Input.Usuario usuario)
        {
            try
            {
                var usr = _usuarioInfrastructure.Get(usuario.UserName);
                if (usr != null && usr.Password == usuario.Password)
                    return usr;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }

        public Common.DataMembers.Output.Usuario Register(Common.DataMembers.Input.Usuario usuario)
        {
            try
            {
                var validation = new UsuarioValidation(_usuarioInfrastructure);
                var results = validation.Validate(usuario);

                //if(!results.IsValid)
                //    throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());
                
                return _usuarioInfrastructure.Create(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                throw;
            }
        }
    }
}
