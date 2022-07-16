using System.Threading;
using System.Threading.Tasks;
using Application.Account.Dto;
using Domain.Common;
using Domain.Common.DependencyLifeTime;

namespace Application.Account.Services
{
    public interface IAcountService : IScopedService
    {
        Task<ApiResult> RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<ApiResult<TokenModel>> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
