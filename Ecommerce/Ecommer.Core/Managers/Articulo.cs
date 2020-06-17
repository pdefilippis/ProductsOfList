using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Common.FaultContracts;
using Ecommerce.Core.Validations;
using Ecommerce.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        private readonly IUsuarioInfrastructure _usuarioInfrastructure;
        public Articulo(IArticuloInfrastructure articuloInfrastructure, IUsuarioInfrastructure usuarioInfrastructure)
        {
            _articuloInfrastructure = articuloInfrastructure;
            _usuarioInfrastructure = usuarioInfrastructure;
        }

        public bool DeclinarPostulacionArticulo(ArticuloPostulacion postulacion)
        {

            if (_articuloInfrastructure.ExistsPostulacion(postulacion))
            {
                _articuloInfrastructure.DeclinarPostulacion(postulacion);
                return true;
            }

            return false;

        }

        public bool Disable(int articulo)
        {

            var item = _articuloInfrastructure.GetById(articulo);
            if (item.Activo)
            {
                _articuloInfrastructure.ChangeStatus(articulo);
                return true;
            }

            return false;

        }

        public bool Enable(int articulo)
        {

            var item = _articuloInfrastructure.GetById(articulo);
            if (!item.Activo)
            {
                _articuloInfrastructure.ChangeStatus(articulo);
                return true;
            }

            return false;

        }

        public ICollection<Common.DataMembers.Output.Articulo> Get()
        {

            return _articuloInfrastructure.Get();

        }

        public ICollection<Common.DataMembers.Output.Articulo> GetAll()
        {

            return _articuloInfrastructure.GetAll();

        }

        public Common.DataMembers.Output.Articulo GetById(int id)
        {
            return _articuloInfrastructure.GetById(id);
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetByUserInteresado(string user)
        {
            var usuario = _usuarioInfrastructure.Get(user);
            return _articuloInfrastructure.GetByUserInteresado(usuario.Id);
        }

        public ICollection<Common.DataMembers.Output.Articulo> GetLote(int lote)
        {

            return _articuloInfrastructure.GetByLote(lote);

        }

        public bool PostularArticulo(ArticuloPostulacion postulacion)
        {

            if (!_articuloInfrastructure.ExistsPostulacion(postulacion))
            {
                _articuloInfrastructure.Postular(postulacion);
                return true;
            }

            return false;

        }

        public Common.DataMembers.Output.Articulo Save(Common.DataMembers.Input.Articulo articulo)
        {

            var validation = new ArticuloValidation(_articuloInfrastructure);
            var results = validation.Validate(articulo);

            //if (!results.IsValid)
                //throw new InvalidDataException(results.Errors.Select(x => x.ErrorMessage).ToList());

            return _articuloInfrastructure.Save(articulo);

        }
    }
}
