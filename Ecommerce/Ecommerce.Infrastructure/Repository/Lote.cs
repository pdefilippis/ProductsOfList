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
    public class Lote : ILoteInfrastructure
    {
        private readonly ITransformMapper _transformMapper;
        private readonly IConnectionContext _context;
        public Lote(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public void ChangeStatus(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Lote.Where(x => x.Id.Equals(id)).FirstOrDefault();

                item.Activo = !item.Activo;
                context.SaveChanges();
            }
        }

        public Output.Lote Create(Input.Lote lote)
        {
            using (var context = _context.Get())
            {
                var item = new Domain.Models.Lote
                {
                    Descripcion = lote.Descripcion,
                    Activo = true,
                    NombreImagen = lote.NombreImagen,
                    Imagen = lote.Imagen
                };

                context.Add(item);
                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Lote, Output.Lote>(item);
            }
        }

        public void Delete(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Lote.Where(x => x.Id.Equals(id)).FirstOrDefault();
                item.Activo = false;
                context.SaveChanges();
            }
        }

        public ICollection<Output.Lote> Get()
        {
            using (var context = _context.Get())
            {
                var items = context.Lote.Where(x => x.Activo).ToList();
                return _transformMapper.Transform<List<Domain.Models.Lote>, ICollection<Output.Lote>>(items);
            }
        }

        public Output.Lote GetById(int id)
        {
            using (var context = new Domain.Models.ProductsManagerContext())
            {
                var item = context.Lote.Where(x => x.Id.Equals(id)).FirstOrDefault();
                return _transformMapper.Transform<Domain.Models.Lote, Output.Lote>(item);
            }
        }

        public Output.Lote Save(Input.Lote lote)
        {
            if (lote.Id.HasValue)
                return Update(lote);
            else
                return Create(lote);
        }

        public Output.Lote Update(Input.Lote lote)
        {
            using (var context = _context.Get())
            {
                var item = context.Lote.Where(x => x.Id.Equals(lote.Id)).FirstOrDefault();

                item.Descripcion = lote.Descripcion;
                item.NombreImagen = lote.NombreImagen;
                item.Imagen = lote.Imagen;

                context.SaveChanges();
                return _transformMapper.Transform<Domain.Models.Lote, Output.Lote>(item);
            }
        }
    }
}
