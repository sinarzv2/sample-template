using System.Threading;
using System.Threading.Tasks;
using Application.Dto.UserDto;
using Domain.Common;
using Domain.Common.DependencyLifeTime;

namespace Application.Services.AcountServices
{
    public interface IAcountService : IScopedService
    {
        Task<ApiResult> RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<ApiResult<TokenModel>> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
