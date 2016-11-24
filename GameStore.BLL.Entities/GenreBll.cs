using GameStore.BLL.Entities.Common;
using GameStore.BLL.Infrastructure.Mappers;
using GameStore.DAL.Models;

namespace GameStore.BLL.Entities
{
    public class GenreBll : BaseModelBll<int>, IMapFrom<Genre>
    {
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}