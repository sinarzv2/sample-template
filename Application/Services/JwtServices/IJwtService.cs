using System.Threading.Tasks;
using Domain.IdentityModel;

namespace Application.Services.JwtServices
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}
