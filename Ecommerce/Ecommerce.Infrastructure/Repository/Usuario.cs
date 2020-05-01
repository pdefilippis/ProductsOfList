using System;
using System.Collections.Generic;
using System.Text;
using Input = Ecommerce.Common.DataMembers.Input;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;
using System.Linq;
using Ecommerce.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repository
{
    public class Usuario : IUsuarioInfrastructure
    {
        private readonly IConnectionContext _context;
        private readonly ITransformMapper _transformMapper;
        public Usuario(ITransformMapper transformMapper, IConnectionContext context)
        {
            _transformMapper = transformMapper;
            _context = context;
        }

        public Output.Usuario ChangePassword(string usuario, string password)
        {
            using (var context = new Domain.Models.ProductsManagerContext())
            {
                var item = context.Usuario.Where(x => x.Usuario1.Equals(usuario)).FirstOrDefault();
                item.Clave = password;
                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Usuario, Output.Usuario>(item);
            }
        }

        public Output.Usuario Create(Input.Usuario usuario)
        {
            using (var context = _context.Get())
            {
                var item = new Domain.Models.Usuario
                {
                    Apellido = usuario.Apellido,
                    Clave = usuario.Password,
                    Nombre = usuario.Nombre,
                    Usuario1 = usuario.UserName
                };

                context.Add(item);
                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Usuario, Output.Usuario>(item);
            }
        }

        public Output.Usuario Get(string usuario)
        {
            using (var context = _context.Get())
            {
                var item = context.Usuario.Where(x => x.Usuario1.Equals(usuario)).FirstOrDefault();
                return _transformMapper.Transform<Domain.Models.Usuario, Output.Usuario>(item);
            }
        }

        public Output.Usuario Save(Input.Usuario usuario)
        {
            if (Exists(usuario.UserName))
                return Update(usuario);
            else
                return Create(usuario);
        }

        public Output.Usuario Update(Input.Usuario usuario)
        {
            using (var context = _context.Get())
            {
                var item = context.Usuario.Where(x => x.Usuario1.Equals(usuario)).FirstOrDefault();

                item.Apellido = usuario.Apellido;
                item.Nombre = usuario.Nombre;

                context.SaveChanges();

                return _transformMapper.Transform<Domain.Models.Usuario, Output.Usuario>(item);
            }
        }

        private bool Exists(string usuario)
        {
            return Get(usuario) != null;
        }
    }
}
