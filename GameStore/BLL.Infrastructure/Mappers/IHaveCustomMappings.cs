using AutoMapper;

namespace BLL.Infrastructure.Mappers
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}