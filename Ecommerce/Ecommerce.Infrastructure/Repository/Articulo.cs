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
                    Precio = articulo.Precio,
                    Marca = articulo.Marca
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
                var items = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Activo).ToList();

                return _transformMapper.Transform<List<Domain.Models.Articulo>, ICollection<Output.Articulo>>(items);
            }
        }

        public Output.Articulo GetById(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Id.Equals(id))
                    .FirstOrDefault();

                return _transformMapper.Transform<Domain.Models.Articulo, Output.Articulo>(item);
            }
        }

        public ICollection<Output.Articulo> GetByLote(int lote)
        {
            using (var context = _context.Get())
            {
                var items = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Activo && x.IdLote.Equals(lote))
                    .ToList();

                return _transformMapper.Transform<List<Domain.Models.Articulo>, ICollection<Output.Articulo>>(items);
            }
        }

        public void Postular(Input.ArticuloPostulacion postulacion)
        {
            using (var context = _context.Get())
            {
                var item = new Domain.Models.Solicitud
                {
                    IdArticulo = postulacion.IdArticulo,
                    IdUsuario = postulacion.IdUsuario
                };

                context.Solicitud.Add(item);
                context.SaveChanges();
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
                var item = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Id.Equals(articulo.Id))
                    .FirstOrDefault();

                item.IdLote = articulo.IdLote;
                item.IdTipo = articulo.IdTipo;
                item.NumeroSerie = articulo.NroSerie;
                item.Precio = articulo.Precio;
                item.Marca = articulo.Marca;

                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Articulo, Output.Articulo>(item);
            }
        }

        public bool ExistsPostulacion(Input.ArticuloPostulacion postulacion)
        {
            using (var context = _context.Get())
            {
                return context.Solicitud.Any(x => x.IdArticulo.Equals(postulacion.IdArticulo) &&
                    x.IdUsuario.Equals(postulacion.IdUsuario));
            }
        }

        public void DeclinarPostulacion(Input.ArticuloPostulacion postulacion)
        {
            using (var context = _context.Get())
            {
                var items = context.Solicitud.Where(x => x.IdArticulo.Equals(postulacion.IdArticulo) &&
                x.IdUsuario.Equals(postulacion.IdUsuario)).ToList();

                items.ForEach(x => context.Solicitud.Remove(x));
                context.SaveChanges();
            }
        }

        public void AdjudicarArticulo(int idArticulo, int idUsuario)
        {
            using (var context = _context.Get())
            {
                var art = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Id.Equals(idArticulo))
                    .FirstOrDefault();

                art.UsuarioAdjudicado = idUsuario;
                context.SaveChanges();
            }
        }

        public void ChangeStatus(int id)
        {
            using (var context = _context.Get())
            {
                var item = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .Where(x => x.Id.Equals(id))
                    .FirstOrDefault();

                item.Activo = !item.Activo;
                context.SaveChanges();
            }
        }

        public ICollection<Output.Articulo> GetAll()
        {
            using (var context = _context.Get())
            {
                var items = context.Articulo
                    .Include("IdLoteNavigation")
                    .Include("IdTipoNavigation")
                    .ToList();

                return _transformMapper.Transform<List<Domain.Models.Articulo>, ICollection<Output.Articulo>>(items);
            }
        }

        public ICollection<Output.Articulo> GetPostulados(int idUsuario)
        {
            throw new NotImplementedException();
            //using (var context = _context.Get())
            //{
            //    var items = context.Solicitud.Where(x => x.)
            //}
        }
    }
}
