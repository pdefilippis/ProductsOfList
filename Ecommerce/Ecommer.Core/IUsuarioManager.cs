using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IUsuarioManager
    {
        Member.Output.Usuario Login(Member.Input.Usuario usuario);
        Member.Output.Usuario Register(Member.Input.Usuario usuario);
        Member.Output.Usuario ChangePassword(Member.Input.ChangePassword usuario);
        ICollection<Member.Output.Usuario> Get();
        ICollection<Member.Output.Usuario> GetAll();
        Member.Output.Usuario GetById(int id);
        void Enable(int id);
        void Disable(int id);
    }
}
