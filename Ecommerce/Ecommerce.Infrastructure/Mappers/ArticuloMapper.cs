using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;
using System.Linq;

namespace Ecommerce.Infrastructure.Mappers
{
    public partial class TransformMapper
    {
        protected static void InitializeArticulo(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.Articulo, Output.Articulo>()
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.NumeroSerie, opt => opt.MapFrom(src => src.NumeroSerie))
                .ForMember(d => d.Precio, opt => opt.MapFrom(src => src.Precio))
                .ForMember(d => d.UsuarioAdjudicado, opt => opt.MapFrom(src => src.UsuarioAdjudicadoNavigation))
                .ForMember(d => d.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(d => d.Lote, opt => opt.MapFrom(src => src.IdLoteNavigation))
                .ForMember(d => d.Tipo, opt => opt.MapFrom(src => src.IdTipoNavigation))
                .ForMember(d => d.Marca, opt => opt.MapFrom(src => src.IdMarcaNavigation))
                .ForMember(d => d.UsuariosInteresados, opt => opt.MapFrom(src => src.Solicitud.Where(x => x.IdArticulo.Equals(src.Id)).Select(x => x.IdUsuarioNavigation)))
                .ReverseMap();

           
        }
    }
}
