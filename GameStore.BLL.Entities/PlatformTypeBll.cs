using GameStore.BLL.Entities.Common;
using GameStore.BLL.Infrastructure.Mappers;
using GameStore.DAL.Models;

namespace GameStore.BLL.Entities
{
    public class PlatformTypeBll : BaseModelBll<int>, IMapFrom<PlatformType>
    {
        public string Type { get; set; }
    }
}