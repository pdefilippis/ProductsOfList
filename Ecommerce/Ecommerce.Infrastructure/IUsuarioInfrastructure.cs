using System;
using System.Collections.Generic;
using System.Text;
using Input = Ecommerce.Common.DataMembers.Input;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;
using System.Linq;
using Ecommerce.Infrastructure.Mappers;

namespace Ecommerce.Infrastructure
{
    public interface IUsuarioInfrastructure
    {
        Output.Usuario Get(string usuario);
        Output.Usuario GetByMail(string eMail);
        Output.Usuario Create(Input.Usuario usuario);
        Output.Usuario Update(Input.Usuario usuario);
        Output.Usuario Save(Input.Usuario usuario);
        Output.Usuario ChangePassword(string usuario, string password);
        ICollection<Output.Usuario> GetByArticulo(int idArticulo);
        void RegistrarLogin(int idUsuario);
        ICollection<Output.Usuario> Get();
        ICollection<Output.Usuario> GetAll();
        Output.Usuario GetById(int id);
        void ChangeStatus(int id);
        void RecoverPasswordToken(int idUsuario, string token);
        ICollection<string> GetTokenValid(int idUsuario);
    }
}
