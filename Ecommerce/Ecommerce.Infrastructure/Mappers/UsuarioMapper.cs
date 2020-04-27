using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Output = Ecommerce.Common.DataMembers.Output;
using Domain = Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Mappers
{
    class UsuarioMapper
    {
    }
    public partial class TransformMapper
    {
        protected static void InitializeUsuario(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Domain.Models.Usuario, Output.Usuario>()
                .ForMember(d => d.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(d => d.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(d => d.Password, opt => opt.MapFrom(src => src.Clave))
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Usuario1))
                .ReverseMap();


        }
    }
}
