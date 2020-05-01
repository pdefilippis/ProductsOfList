using System;
using System.Collections.Generic;
using System.Text;
using Input = Ecommerce.Common.DataMembers.Input;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;
using System.Linq;
using Ecommerce.Infrastructure.Mappers;
using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repository
{
    public class Articulo : IArticuloInfrastructure
    {
        private readonly ITransformMapper _transformMapper;
        private readonly IConnectionContext _context;
        public Articulo(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public Output.Articulo Create(Input.Articulo articulo)
        {
            using (var context = _context.Get())
            {
                var item = new Domain.Models.Articulo
                {
                    Activo = true,
                    Descripcion = articulo.Descripcion,
                    IdLote = articulo.IdLote,
                    IdTipo = articulo.IdTipo,
                    NumeroSerie = articulo.NroSerie,
                    Precio = articulo.Precio
                };

                context.Add(item);
                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Articulo, Output.Articulo>(item);
            }
        }

        public void Delete(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Articulo.Where(x => x.Id.Equals(id)).FirstOrDefault();
                item.Activo = false;
                context.SaveChanges();
            }
        }

        public ICollection<Output.Articulo> Get()
        {
            using (var context = _context.Get())
            {
                var items = context.Articulo.Where(x => x.Activo).ToList();
                return _transformMapper.Transform<List<Domain.Models.Articulo>, ICollection<Output.Articulo>>(items);
            }
        }

        public Output.Articulo GetById(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Articulo.Where(x => x.Id.Equals(id)).FirstOrDefault();
                return _transformMapper.Transform<Domain.Models.Articulo, Output.Articulo>(item);
            }
        }

        public ICollection<Output.Articulo> GetByLote(int lote)
        {
            using (var context = _context.Get())
            {
                var items = context.Articulo.Where(x => x.Activo && x.IdLote.Equals(lote)).ToList();
                return _transformMapper.Transform<List<Domain.Models.Articulo>, ICollection<Output.Articulo>>(items);
            }
        }

        public Output.Articulo Save(Input.Articulo articulo)
        {
            if (articulo.Id.HasValue)
                return Update(articulo);
            else
                return Create(articulo);
        }

        public Output.Articulo Update(Input.Articulo articulo)
        {
            using (var context = _context.Get())
            {
                var item = context.Articulo.Where(x => x.Id.Equals(articulo.Id)).FirstOrDefault();

                item.IdLote = articulo.IdLote;
                item.IdTipo = articulo.IdTipo;
                item.NumeroSerie = articulo.NroSerie;
                item.Precio = articulo.Precio;

                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Articulo, Output.Articulo>(item);
            }
        }
    }
}
