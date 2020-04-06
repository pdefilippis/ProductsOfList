using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IUsuarioManager
    {
        Member.Output.Usuario Login(Member.Input.Usuario usuario);
    }
}
