using Bll.Core.Entities.Common;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace Bll.Core.Entities
{
    public class GenreBll : BaseModelBll<int>, IMapFrom<Genre>
    {
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}