using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Mappers
{
    public interface ITransformMapper
    {
        TDestino Transform<TOrigen, TDestino>(TOrigen model);
    }

    public partial class TransformMapper : ITransformMapper
    {
        private static IMapper _mapper;
        public static void Initialize(IMapperConfigurationExpression ce)
        {
            InitializeArticulo(ce);
            InitializeLote(ce);
            InitializeArticuloTipo(ce);
            InitializeUsuario(ce);
            InitializeEstado(ce);
        }

        public static void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestino Transform<TOrigen, TDestino>(TOrigen model)
        {
            return _mapper.Map<TOrigen, TDestino>(model);
        }
    }
}
