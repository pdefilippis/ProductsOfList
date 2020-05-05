using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.Core.Validations
{
    public class LoteValidation : AbstractValidator<Lote>
    {
        private readonly ILoteInfrastructure _loteInfrastructure;
        public LoteValidation(ILoteInfrastructure loteInfrastructure)
        {
            _loteInfrastructure = loteInfrastructure;

            RuleFor(x => x.Descripcion).NotNull().NotEmpty().WithMessage("Debe especificar una descripcion al lote.");
            RuleFor(x => x.Descripcion).Must(DescripcionDuplicada).WithMessage("La fecha desde debe ser mayor que la fecha actual.");
        }

        public bool DescripcionDuplicada(Lote lote, string descripcion)
        {
            var items = _loteInfrastructure.GetByDescripcion(descripcion);

            return items.ToList().Any(x => x.Id != lote.Id);
        }
    }
}
