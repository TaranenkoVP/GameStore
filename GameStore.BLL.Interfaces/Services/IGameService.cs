using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.Entities;

namespace GameStore.BLL.Interfaces.Services
{
    public interface IGameService
    {
        Task<List<GameBll>> GetAllAsync();
        Task<GameBll> GetDetailsByKeyAsync(string gamekey);
        Task<List<GameBll>> GetByGenreAsync(string name);
        Task<List<GameBll>> GetByPlatformTypeAsync(string platformType);
        Task<byte[]> DownloadAsync(string gamekey);
        Task AddAsync(GameBll entity);
        Task UpdateAsync(GameBll entity);
        Task DeleteAsync(string gamekey);
    }
}