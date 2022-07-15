using System.Threading.Tasks;
using Application.Dto.UserDto;
using Domain.Common.DependencyLifeTime;
using Domain.Entities.IdentityModel;

namespace Application.Services.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenModel> GenerateAsync(User user);
    }
}
