using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Input;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Infrastructure;

namespace Ecommerce.Core.Managers
{
    public class Articulo : IArticuloManager
    {
        private readonly IArticuloInfrastructure _articuloInfrastructure;
        public Articulo(IArticuloInfrastructure articuloInfrastructure)
        {
            _articuloInfrastructure = articuloInfrastructure;
        }

        public void DeclinarPostulacionArticulo(ArticuloPostulacion postulacion)
        {
            if (_articuloInfrastructure.ExistsPostulacion(postulacion))
                _articuloInfrastructure.DeclinarPostulacion(postulacion);
        }

        public void Disable(int articulo)
        {
            var item = _articuloInfrastructure.GetById(articulo);
            if (item.Activo)
                _articuloInfrastructure.ChangeStatus(articulo);
        }

        public void Enable(int articulo)
        {
            var item = _articuloInfrastructure.GetById(articulo);
            if (!item.Activo)
                _articuloInfrastructure.ChangeStatus(articulo);
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

        public ICollection<Common.DataMembers.Output.Articulo> GetLote(int lote)
        {
            return _articuloInfrastructure.GetByLote(lote);
        }

        public void PostularArticulo(ArticuloPostulacion postulacion)
        {
            if (!_articuloInfrastructure.ExistsPostulacion(postulacion))
                _articuloInfrastructure.Postular(postulacion);
        }

        public Common.DataMembers.Output.Articulo Save(Common.DataMembers.Input.Articulo articulo)
        {
            return _articuloInfrastructure.Save(articulo);
        }
    }
}
