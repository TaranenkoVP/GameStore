using System.Collections.Generic;
using BLL.Core.Entities;
using BLL.Core.Entities.GameBll;

namespace Bll.Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameBll> GetAll();
        GameDetailsBll GetDetailsByKey(string gamekey);
        IEnumerable<CommentBll> GetComments(string gamekey);
        IEnumerable<GameBll> GetByGenre(string name);
        IEnumerable<GameBll> GetByPlatformType(string platformType);
        byte[] Download(string gamekey);
        void Add(GameDetailsBll entity);
        void Update(GameDetailsBll entity);
        void Delete(string gamekey);
    }
}