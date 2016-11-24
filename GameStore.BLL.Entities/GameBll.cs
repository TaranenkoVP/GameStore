using System.Collections.Generic;
using GameStore.BLL.Entities.Common;
using GameStore.BLL.Infrastructure.Mappers;
using GameStore.DAL.Models;

namespace GameStore.BLL.Entities
{
    public class GameBll : BaseModelBll<int>, IMapFrom<Game>
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DownloadPath { get; set; }

        public ICollection<CommentBll> Comments { get; set; }

        public ICollection<GenreBll> Genres { get; set; }

        public ICollection<PlatformTypeBll> PlatformTypes { get; set; }
    }
}