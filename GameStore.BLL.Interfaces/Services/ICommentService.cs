using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Entities;

namespace GameStore.BLL.Interfaces.Services
{
    public interface ICommentService
    {
        Task<List<CommentBll>> GetCommentsAsync(string gamekey);
        Task AddAsync(CommentBll entity);
        Task UpdateAsync(CommentBll entity);
        Task DeleteAsync(int key);
    }
}