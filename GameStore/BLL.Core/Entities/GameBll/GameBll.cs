using Bll.Core.Entities.Common;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace BLL.Core.Entities.GameBll
{
    public class GameBll : BaseModelBll<int>, IMapFrom<Game>
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}