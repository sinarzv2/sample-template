using System;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Common.DependencyLifeTime;
using Common.Models;

namespace Application.AccountApplication.Services
{
    public interface IAcountService : IScopedService
    {
        Task<ApiResult> RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<ApiResult<TokenModel>> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
        Task<ApiResult<TokenModel>> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken);
        Task<ApiResult> Revoke(Guid userId, CancellationToken cancellationToken);
    }
}
