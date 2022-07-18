using System;
using System.Threading;
using System.Threading.Tasks;
using Common.DependencyLifeTime;
using Infrastructure.IRepository;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IScopedService
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
