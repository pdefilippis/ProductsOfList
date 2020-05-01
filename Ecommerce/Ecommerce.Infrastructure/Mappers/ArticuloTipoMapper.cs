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
        protected static void InitializeArticuloTipo(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.ArticuloTipo, Output.ArticuloTipo>()
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }
}
