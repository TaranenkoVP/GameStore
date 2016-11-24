using AutoMapper;

namespace GameStore.BLL.Infrastructure.Mappers
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}