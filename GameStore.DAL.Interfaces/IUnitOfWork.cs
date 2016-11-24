using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Interfaces.Repositories;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task CommitAsync();
    }
}
