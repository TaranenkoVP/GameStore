using AutoMapper;

namespace GameStore.WEB.Infrastructure.Mappers
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}