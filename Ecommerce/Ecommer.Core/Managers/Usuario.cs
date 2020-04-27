using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;

namespace Ecommerce.Core.Managers
{
    public class Usuario : IUsuarioManager
    {
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        public Usuario(IUsuarioInfrastructure usuarioInfrastructure)
        {
            _usuarioInfrastructure = usuarioInfrastructure;
        }

        public Common.DataMembers.Output.Usuario ChangePassword(Common.DataMembers.Input.ChangePassword usuario)
        {
            var usr = _usuarioInfrastructure.Get(usuario.UserName);
            if (usr.Password == usuario.OldPassword)
                return _usuarioInfrastructure.ChangePassword(usuario.UserName, usuario.NewPassword);

            return null;
        }

        public Common.DataMembers.Output.Usuario Login(Common.DataMembers.Input.Usuario usuario)
        {
            var usr = _usuarioInfrastructure.Get(usuario.UserName);
            if (usr != null && usr.Password == usuario.Password)
                return usr;

            return null;
        }

        public Common.DataMembers.Output.Usuario Register(Common.DataMembers.Input.Usuario usuario)
        {
            return _usuarioInfrastructure.Create(usuario);
        }
    }
}
