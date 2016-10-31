using System.Collections.Generic;
using Bll.Core.Entities.Common;
using BLL.Core.Entities;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace Bll.Core.Entities.GameBll
{
    public class GameWithCommentsBll : BaseModelBll<int>, IMapFrom<Game>
    {
        public ICollection<CommentBll> Comments { get; set; }
    }
}