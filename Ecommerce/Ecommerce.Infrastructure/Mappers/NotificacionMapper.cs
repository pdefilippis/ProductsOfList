using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Mappers
{
    public partial class TransformMapper
    {
        protected static void InitializeNotificacion(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.Notificaciones, Output.Notificacion>()
                .ForMember(d => d.Leido, opt => opt.MapFrom(src => src.Leido))
                .ForMember(d => d.Stamp, opt => opt.MapFrom(src => src.Stamp))
                .ForMember(d => d.Articulo, opt => opt.MapFrom(src => src.IdArticuloNavigation))
                .ForMember(d => d.Usuario, opt => opt.MapFrom(src => src.IdUsuarioNavigation))
                .ReverseMap();
        }
    }
}
