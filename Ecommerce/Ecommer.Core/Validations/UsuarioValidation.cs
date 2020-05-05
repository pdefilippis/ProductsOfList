using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Core.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        public UsuarioValidation(IUsuarioInfrastructure usuarioInfrastructure)
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Se debe especificar un usuario.");
            RuleFor(x => x.Apellido).NotNull().NotEmpty().WithMessage("Se debe especificar el apellido.");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Se debe especificar el nombre.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Se debe especificar una contraseña.");
            RuleFor(x => x.UserName).Must(UsuarioRepetido).WithMessage("Ya existe el usuario.");
        }

        public bool UsuarioRepetido(Usuario usuario, string userName)
        {
            var item = _usuarioInfrastructure.Get(userName);

            return item != null;
        }
    }
}
