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
    public class Notificaciones : INotificacionesInfrastructure
    {
        private readonly ITransformMapper _transformMapper;
        private readonly IConnectionContext _context;
        public Notificaciones(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public void Create(Input.Notificacion notificacion)
        {
            using (var context = _context.Get())
            {
                var item = new Domain.Models.Notificaciones
                {
                    IdArticulo = notificacion.IdArticulo,
                    IdUsuario = notificacion.IdUsuario
                };

                context.Notificaciones.Add(item);
                context.SaveChanges();
            }
        }

        public ICollection<Output.Notificacion> GetByUser(int idUsuario)
        {
            using (var context = _context.Get())
            {
                var items = context.Notificaciones
                    .Include("IdArticuloNavigation")
                    .Include("IdArticuloNavigation.IdTipoNavigation")
                    .Include("IdArticuloNavigation.IdLoteNavigation")
                    .Include("IdArticuloNavigation.UsuarioAdjudicadoNavigation")
                    .Include("IdUsuarioNavigation")
                    .Where(x => x.IdUsuario.Equals(idUsuario) &&
                    x.Leido.HasValue && !x.Leido.Value).ToList();

                return _transformMapper.Transform<List<Domain.Models.Notificaciones>, ICollection<Output.Notificacion>>(items);
            }
        }

        public ICollection<Output.Notificacion> GetByUser(string userName)
        {
            using (var context = _context.Get())
            {
                var items = context.Notificaciones
                    .Include("IdArticuloNavigation")
                    .Include("IdArticuloNavigation.IdTipoNavigation")
                    .Include("IdArticuloNavigation.IdLoteNavigation")
                    .Include("IdArticuloNavigation.UsuarioAdjudicadoNavigation")
                    .Include("IdUsuarioNavigation")
                    .Where(x => x.IdUsuarioNavigation.Usuario1.Equals(userName) &&
                    x.Leido.HasValue && !x.Leido.Value).ToList();

                return _transformMapper.Transform<List<Domain.Models.Notificaciones>, ICollection<Output.Notificacion>>(items);
            }
        }

        public void RecordReading(int idUsuario)
        {
            using (var context = _context.Get())
            {
                var items = context.Notificaciones
                    .Where(x => x.IdUsuario.Equals(idUsuario)).ToList();

                items.ForEach(x => x.Leido = true);

                context.SaveChanges();
            }
        }

        public void RecordReading(string userName)
        {
            using (var context = _context.Get())
            {
                var items = context.Notificaciones
                    .Where(x => x.IdUsuarioNavigation.Usuario1.Equals(userName)).ToList();

                items.ForEach(x => x.Leido = true);

                context.SaveChanges();
            }
        }
    }
}
