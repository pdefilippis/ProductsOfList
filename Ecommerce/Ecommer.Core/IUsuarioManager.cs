﻿using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IUsuarioManager
    {
        Member.Output.Usuario Login(Member.Input.Usuario usuario);
        Member.Output.Usuario Register(Member.Input.Usuario usuario);
        Member.Output.Usuario Save(Member.Input.Usuario usuario);
        Member.Output.Usuario ChangePassword(Member.Input.ChangePassword usuario);
        ICollection<Member.Output.Usuario> Get();
        ICollection<Member.Output.Usuario> GetAll();
        Member.Output.Usuario GetById(int id);
        Member.Output.Usuario GetByName(string name);
        bool Enable(int id);
        bool Disable(int id);
        bool CleanPassword(string email);
        void RecoverPasswordNotification(string email);
        bool CheckRecoverPassword(Member.Input.RecoverPassword recoverPassword);
    }
}
