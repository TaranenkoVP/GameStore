using Bll.Core.Entities.Common;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace BLL.Core.Entities
{
    public class CommentBll : BaseModelBll<int>, IMapFrom<Comment>
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int GameId { get; set; }

        public int? AuthorId { get; set; }
    }
}