using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(configuration =>
            {
                
            })
            .CreateMapper();
    }
}
