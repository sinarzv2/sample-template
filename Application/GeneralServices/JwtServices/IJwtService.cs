using System.Threading.Tasks;
using Application.Account.Dto;
using Domain.Common.DependencyLifeTime;
using Domain.Entities.IdentityModel;

namespace Application.GeneralServices.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenModel> GenerateAsync(User user);
    }
}
