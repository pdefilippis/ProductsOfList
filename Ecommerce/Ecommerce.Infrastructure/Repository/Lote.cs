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
                item.Actualizacion = DateTime.Now;
                context.SaveChanges();
            }
        }

        public void ChangeStatus(int idLote, string newStatus)
        {
            using (var context = _context.Get())
            {
                var item = context.Lote.Where(x => x.Id.Equals(idLote)).FirstOrDefault();
                var estado = context.Estado.Where(x => x.Activo.Value && x.Codigo.Equals(newStatus)).FirstOrDefault();

                item.IdEstado = estado.Id;
                context.SaveChanges();
            }
        }

        public Output.Lote Create(Input.Lote lote)
        {
            using (var context = _context.Get())
            {
                var estado = context.Estado.Where(x => x.Activo.Value && x.Codigo.Equals(Ecommerce.Common.Constant.Properties.Estado.Abierto)).FirstOrDefault();

                var item = new Domain.Models.Lote
                {
                    Descripcion = lote.Descripcion,
                    Activo = true,
                    NombreImagen = lote.NombreImagen,
                    Imagen = lote.Imagen,
                    IdEstado = estado.Id
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
                item.Actualizacion = DateTime.Now;
                context.SaveChanges();
            }
        }

        public ICollection<Output.Lote> Get()
        {
            using (var context = _context.Get())
            {
                var items = context.Lote
                    .Include("Articulo")
                    .Include("IdEstadoNavigation")
                    .Include("Articulo.UsuarioAdjudicadoNavigation")
                    .Include("Articulo.Solicitud")
                    .Include("Articulo.Solicitud.IdUsuarioNavigation")
                    .Include("IdEstadoNavigation")
                    .Where(x => x.Activo).ToList();
                return _transformMapper.Transform<List<Domain.Models.Lote>, ICollection<Output.Lote>>(items);
            }
        }

        public ICollection<Output.Lote> GetAll()
        {
            using (var context = _context.Get())
            {
                var items = context.Lote
                    .Include("Articulo")
                    .Include("IdEstadoNavigation")
                    .Include("Articulo.UsuarioAdjudicadoNavigation")
                    .Include("Articulo.Solicitud")
                    .Include("Articulo.Solicitud.IdUsuarioNavigation")
                    .Include("IdEstadoNavigation")
                    .ToList();
                return _transformMapper.Transform<List<Domain.Models.Lote>, ICollection<Output.Lote>>(items);
            }
        }

        public ICollection<Output.Lote> GetByDescripcion(string descripcion)
        {
            using (var context = _context.Get())
            {
                var items = context.Lote
                    .Include("Articulo")
                    .Include("IdEstadoNavigation")
                    .Include("Articulo.UsuarioAdjudicadoNavigation")
                    .Include("Articulo.Solicitud")
                    .Include("Articulo.Solicitud.IdUsuarioNavigation")
                    .Include("IdEstadoNavigation")
                    .Where(x => x.Descripcion.ToLower().Equals(descripcion.ToLower())).ToList();
                return _transformMapper.Transform<List<Domain.Models.Lote>, ICollection<Output.Lote>>(items);
            }
        }

        public Output.Lote GetById(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Lote
                    .Include("Articulo")
                    .Include("IdEstadoNavigation")
                    .Include("Articulo.UsuarioAdjudicadoNavigation")
                    .Include("Articulo.Solicitud")
                    .Include("Articulo.Solicitud.IdUsuarioNavigation")
                    .Include("IdEstadoNavigation")    
                    .Where(x => x.Id.Equals(id)).FirstOrDefault();
                var test = _transformMapper.Transform<Domain.Models.Lote, Output.Lote>(item);
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
                var item = context.Lote
                    .Include("Articulo")
                    .Include("IdEstadoNavigation")
                    .Include("Articulo.UsuarioAdjudicadoNavigation")
                    .Include("Articulo.Solicitud")
                    .Include("Articulo.Solicitud.IdUsuarioNavigation")
                    .Include("IdEstadoNavigation")
                    .Where(x => x.Id.Equals(lote.Id)).FirstOrDefault();

                item.Descripcion = lote.Descripcion;
                item.NombreImagen = lote.NombreImagen;
                item.Imagen = lote.Imagen;
                item.Actualizacion = DateTime.Now;

                context.SaveChanges();
                return _transformMapper.Transform<Domain.Models.Lote, Output.Lote>(item);
            }
        }
    }
}
