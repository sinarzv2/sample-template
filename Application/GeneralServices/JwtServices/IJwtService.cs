using System.Security.Claims;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Common.DependencyLifeTime;
using Common.Models;
using Domain.Entities.IdentityModel;

namespace Application.GeneralServices.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenModel> GenerateAsync(User user);

        ApiResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
    }
}
