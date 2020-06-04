using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Common.DataMembers.Output;
using Ecommerce.Test.ModelFactory.Domain;

namespace Ecommerce.Test.ModelFactory.Output
{
    public static class UsuarioOutputFactory
    {
        private static bool load = true;

        public static Usuario Get()
        {
            //if (!load) return null;
            load = !load;

            var item = UsuarioFactory.Get();

            return new Usuario
            {
                Activo = item.Activo.Value,
                Apellido = item.Apellido,
                Creacion = item.Creacion,
                Email = item.Mail,
                EsAdministrador = item.Administrador,
                Id = item.Id,
                Nombre = item.Nombre,
                Password = item.Clave,
                UltimoIngreso = item.UltimoIngreso,
                UserName = item.Usuario1
            };
        }

        public static List<Usuario> GetList()
        {
            return new List<Usuario> { Get() };
        }
    }
}
