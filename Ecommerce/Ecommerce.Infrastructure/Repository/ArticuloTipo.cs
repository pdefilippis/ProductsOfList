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
    public class ArticuloTipo : IArticuloTipoInfrastructure
    {
        private readonly ITransformMapper _transformMapper;
        private readonly IConnectionContext _context;
        public ArticuloTipo(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public ICollection<Output.ArticuloTipo> Get()
        {
            using (var context = _context.Get())
            {
                var items = context.ArticuloTipo.Where(x => x.Activo).ToList();
                return _transformMapper.Transform<List<Domain.Models.ArticuloTipo>, ICollection<Output.ArticuloTipo>>(items);
            }
        }

        public Common.DataMembers.Output.ArticuloTipo GetByCodigo(string codigo)
        {
            using (var context = _context.Get())
            {
                var item = context.ArticuloTipo.Where(x => x.Activo && x.Codigo.Equals(codigo)).FirstOrDefault();
                return _transformMapper.Transform<Domain.Models.ArticuloTipo, Output.ArticuloTipo>(item);
            }
        }

        public Output.ArticuloTipo GetById(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.ArticuloTipo.Where(x => x.Activo && x.Id.Equals(id)).FirstOrDefault();
                return _transformMapper.Transform<Domain.Models.ArticuloTipo, Output.ArticuloTipo>(item);
            }
        }
    }
}
