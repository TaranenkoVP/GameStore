using GameStore.BLL.Entities.Common;
using GameStore.BLL.Infrastructure.Mappers;
using GameStore.DAL.Models;

namespace GameStore.BLL.Entities
{
    public class CommentBll : BaseModelBll<int>, IMapFrom<Comment>
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int GameId { get; set; }

        public int? AuthorId { get; set; }
    }
}