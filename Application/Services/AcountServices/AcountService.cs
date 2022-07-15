using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto.UserDto;
using Application.Services.JwtServices;
using AutoMapper;
using Domain.Common;
using Domain.Common.Constant;
using Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.AcountServices
{
    public class AcountService : IAcountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public AcountService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

      
        public async Task<ApiResult> RegisterUser(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var result = new ApiResult<RegisterRequest>();
            var user = _mapper.Map<User>(registerRequest);
            var identityResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!identityResult.Succeeded)
            {
                var allError = identityResult.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return result;
            }
            var resultUserRole = await _userManager.AddToRoleAsync(user, ConstantRoles.User);
            if (!resultUserRole.Succeeded)
            {
                var allError = resultUserRole.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return result;
            }
            return result;
        }

        public async Task<ApiResult<TokenModel>> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var result = new ApiResult<TokenModel>();
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                result.AddError("نام کاربری و رمز عبور اشتباه است.");
                return result;
            }

            var isPassValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPassValid)
            {
                result.AddError("نام کاربری و رمز عبور اشتباه است.");
                return result;
            }
            result.Data = await _jwtService.GenerateAsync(user);
            return result;
        }
    }
}
