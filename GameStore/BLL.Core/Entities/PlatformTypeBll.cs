using Bll.Core.Entities.Common;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace BLL.Core.Entities
{
    public class PlatformTypeBll : BaseModelBll<int>, IMapFrom<PlatformType>
    {
        public string Type { get; set; }
    }
}