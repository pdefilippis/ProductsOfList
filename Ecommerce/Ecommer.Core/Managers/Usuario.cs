using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Common.FaultContracts;
using Ecommerce.Core.Services;
using Ecommerce.Core.Validations;
using Ecommerce.Infrastructure;
using Microsoft.Extensions.Logging;

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

        public bool CleanPassword(string email)
        {
            var user = _usuarioInfrastructure.GetByMail(email);
            if (user == null) return false;


            throw new NotImplementedException();
        }

        public bool Disable(int id)
        {
            var item = _usuarioInfrastructure.GetById(id);
            if (item != null && item.Activo)
            {
                _usuarioInfrastructure.ChangeStatus(id);
                return true;
            }
            return false;
        }

        public bool Enable(int id)
        {
            var item = _usuarioInfrastructure.GetById(id);
            if (item != null && !item.Activo)
            {
                _usuarioInfrastructure.ChangeStatus(id);
                return true;
            }
            return false;
        }

        public ICollection<Common.DataMembers.Output.Usuario> Get()
        {
            return _usuarioInfrastructure.Get();
        }

        public ICollection<Common.DataMembers.Output.Usuario> GetAll()
        {
            return _usuarioInfrastructure.GetAll();
        }

        public Common.DataMembers.Output.Usuario GetById(int id)
        {
            return _usuarioInfrastructure.GetById(id);
        }

        public Common.DataMembers.Output.Usuario GetByName(string name)
        {
            return _usuarioInfrastructure.Get(name);
        }

        public Common.DataMembers.Output.Usuario Login(Common.DataMembers.Input.Usuario usuario)
        {
            
                var usr = _usuarioInfrastructure.Get(usuario.UserName);
                if (usr != null && usr.Password == usuario.Password)
                {
                    _usuarioInfrastructure.RegistrarLogin(usr.Id);
                    return usr;
                }

                return null;
            
        }

        public void RecoverPasswordNotification(string email)
        {
            var item = _usuarioInfrastructure.GetByMail(email);
            if (item == null) return;

            var token = GenerateToken(6);
            _usuarioInfrastructure.RecoverPasswordToken(item.Id, token);
            new Notifications().SendRecoverPassword(email, token);
        }

        public Common.DataMembers.Output.Usuario Register(Common.DataMembers.Input.Usuario usuario)
        {
            
                var validation = new UsuarioValidation(_usuarioInfrastructure);
                var results = validation.Validate(usuario);

            //if (!results.IsValid)
                //throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

            return _usuarioInfrastructure.Create(usuario);
            
        }

        public Common.DataMembers.Output.Usuario Save(Common.DataMembers.Input.Usuario usuario)
        {
            var validation = new UsuarioValidation(_usuarioInfrastructure);
            var results = validation.Validate(usuario);

            //if (!results.IsValid)
                //throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

            return _usuarioInfrastructure.Save(usuario);
        }

        private static Random random = new Random();
        private string GenerateToken(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool CheckRecoverPassword(RecoverPassword recoverPassword)
        {
            var item = _usuarioInfrastructure.GetByMail(recoverPassword.Email);
            if (item == null) return false;

            var tokens = _usuarioInfrastructure.GetTokenValid(item.Id);

            if(!tokens.Contains(recoverPassword.Token)) return false;

            _usuarioInfrastructure.ChangePassword(item.UserName, recoverPassword.Password);
            return true;
        }
    }
}
