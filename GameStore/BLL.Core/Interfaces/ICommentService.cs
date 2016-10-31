using System.Collections.Generic;
using BLL.Core.Entities;

namespace Bll.Core.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentBll> GetComments(string gamekey);
        void Add(CommentBll entity);
        void Update(CommentBll entity);
        void Delete(string key);
    }
}