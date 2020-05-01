using Ecommerce.Common.DataMembers.Input;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Core.Validations
{
    public class ArticuloValidation : AbstractValidator<Articulo>
    {
        public ArticuloValidation()
        {
            //RuleFor(x => x.Desde).Must(ValidationFechaActual).WithMessage("La fecha desde debe ser mayor que la fecha actual.");
        }
    }
}
