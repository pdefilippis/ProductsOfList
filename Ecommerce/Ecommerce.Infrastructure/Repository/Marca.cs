using System;
using System.Collections.Generic;
using System.Text;
using Input = Ecommerce.Common.DataMembers.Input;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;
using System.Linq;
using Ecommerce.Infrastructure.Mappers;
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repository
{
    public class Marca : IMarcaInfrastructure
    {
        private readonly ITransformMapper _transformMapper;
        private readonly IConnectionContext _context;
        public Marca(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public ICollection<Output.Marca> Get()
        {
            using (var context = _context.Get())
            {
                var items = context.Marca.Where(x => x.Activo.HasValue && x.Activo.Value).ToList();

                return _transformMapper.Transform<List<Domain.Models.Marca>, ICollection<Output.Marca>>(items);
            }
        }
    }
}
