using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;

namespace Ecommerce.Core.Managers
{
    public class Usuario : IUsuarioManager
    {
        public Common.DataMembers.Output.Usuario Login(Common.DataMembers.Input.Usuario usuario)
        {
            Common.DataMembers.Output.Usuario userResult = null;
            if (usuario.UserName.ToLower() == "admin" && usuario.Password == "123456")
            {
                userResult = new Common.DataMembers.Output.Usuario
                {
                    Apellido = "Martinez",
                    Nombre = "Jose",
                    UserName = usuario.UserName,
                    Acciones = new string[] { "ADM", "USR" }
                };
            }

            return userResult;
        }
    }
}
