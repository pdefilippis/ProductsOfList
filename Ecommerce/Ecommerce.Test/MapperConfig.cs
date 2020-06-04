using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Test
{
    public class MapperConfig
    {
        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => {
                Infrastructure.Mappers.TransformMapper.Initialize(cfg);
            });

            var mapper = config.CreateMapper();
            Infrastructure.Mappers.TransformMapper.SetMapper(mapper);
        }
    }
}
