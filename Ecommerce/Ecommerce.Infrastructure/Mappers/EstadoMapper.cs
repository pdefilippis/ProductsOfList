using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Mappers
{
    class EstadoMapper
    {
    }
    public partial class TransformMapper
    {
        protected static void InitializeEstado(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.Estado, Output.Estado>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }
}
