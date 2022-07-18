using System.Threading;
using System.Threading.Tasks;
using Application.Account.Dto;
using Common.DependencyLifeTime;
using Common.Models;

namespace Application.Account.Services
{
    public interface IAcountService : IScopedService
    {
        Task<ApiResult> RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<ApiResult<TokenModel>> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
