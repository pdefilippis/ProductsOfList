using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;

namespace Ecommerce.Test.ModelFactory.Domain
{
    public static class ArticuloFactory
    {

        public static Articulo Get()
        {
            return new Articulo
            {
                Activo = true,
                Descripcion = "Monitor",
                Id = 24,
                IdLote = LoteFactory.Get().Id,
                //IdLoteNavigation = LoteFactory.Get(),
                IdTipo = ArticuloTipoFactory.Get().Id,
                //IdTipoNavigation = ArticuloTipoFactory.Get(),
                Marca = "Lenovo",
                //Notificaciones = NotificacionesFactory.GetList(),
                NumeroSerie = "12325A231",
                Precio = 321,
                //Solicitud = SolicitudFactory.GetList(),
                UsuarioAdjudicado = UsuarioFactory.Get().Id,
                //UsuarioAdjudicadoNavigation = UsuarioFactory.Get()
                
            };
        }

        public static List<Articulo> GetList()
        {
            return new List<Articulo> { Get() };
        }
    }
}
