using AutoMapper;
using BLL.Infrastructure.Mappers;

namespace BLL.Services.Common
{
    public abstract class EntityService
    {
        //todo improve reflection Assembly names
        protected IMapper MapperBll => new AutoMapperConfig("BLL.Core").Configuration.CreateMapper();
    }
}