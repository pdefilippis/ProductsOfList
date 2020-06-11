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
        protected static void InitializeLote(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.Lote, Output.Lote>()
                .ForMember(d => d.Articulos, opt => opt.MapFrom(src => src.Articulo))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Imagen, opt => opt.MapFrom(src => src.Imagen))
                .ForMember(d => d.NombreImagen, opt => opt.MapFrom(src => src.NombreImagen))
                .ForMember(d => d.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(d => d.Actualizacion, opt => opt.MapFrom(src => src.Actualizacion))
                .ForMember(d => d.Creacion, opt => opt.MapFrom(src => src.Creacion))
                .ForMember(d => d.Estado, opt => opt.MapFrom(src => src.IdEstadoNavigation))
                .ReverseMap();
        }
    }
}
