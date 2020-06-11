using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.Core.Validations
{
    public class ArticuloValidation : AbstractValidator<Articulo>
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        public ArticuloValidation(IArticuloInfrastructure articuloInfrastructure)
        {
            _articuloInfrastructure = articuloInfrastructure;

            RuleFor(x => x.Descripcion).NotEmpty().NotNull().WithMessage("Debe especificar una descripcion.");
            RuleFor(x => x.Descripcion).Must(ExistsDescripcion).WithMessage("La descripcion ingresada ya existe en este lote.");
            RuleFor(x => x.NroSerie).Must(ExistsNroSerie).WithMessage("El numero de serie ya existe.");
            RuleFor(x => x.Precio).Must(MinPrice).WithMessage("El precio debe ser mayor que cero.");
        }

        public bool ExistsDescripcion(Articulo articulo, string descripcion)
        {
            var items = _articuloInfrastructure.Get();
            return items != null && items.ToList().Any(x =>
                x.Descripcion.Equals(descripcion) &&
                x.Lote.Id.Equals(articulo.IdLote) &&
                !x.Id.Equals(articulo.Id));
        }

        public bool ExistsNroSerie(Articulo articulo, string nroSerie)
        {
            var items = _articuloInfrastructure.Get();

            return items.ToList().Any(x =>
                x.NumeroSerie.Equals(nroSerie) &&
                !x.Id.Equals(articulo.Id));
        }

        public bool MinPrice(Articulo articulo, decimal precio)
        {
            return precio > 0;
        }
    }
}
