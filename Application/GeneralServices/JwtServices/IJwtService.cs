using System.Threading.Tasks;
using Application.Account.Dto;
using Common.DependencyLifeTime;
using Domain.Entities.IdentityModel;

namespace Application.GeneralServices.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenModel> GenerateAsync(User user);
    }
}
