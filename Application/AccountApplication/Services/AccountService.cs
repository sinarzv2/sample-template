using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Application.GeneralServices.JwtServices;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Domain.Entities.IdentityModel;
using Infrastructure.UnitOfWork;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.AccountApplication.Services
{
    public class AccountService : IAcountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService,IOptionsSnapshot<SiteSettings> siteSetting, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _jwtSettings = siteSetting.Value.JwtSettings;
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
                result.AddError(Errors.InvalidUsernameOrPassword);
                return result;
            }

            var isPassValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPassValid)
            {
                result.AddError(Errors.InvalidUsernameOrPassword);
                return result;
            }

            var tokenModel = await _jwtService.GenerateAsync(user);
            user.RefreshToken = tokenModel.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.ExpirationRefreshTimeDays);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            result.Data = tokenModel;
            return result;
        }

        public async Task<ApiResult<TokenModel>> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = new ApiResult<TokenModel>();
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (!principal.IsSuccess)
            {
                result.AddErrors(principal.Errors);
                return result;
            }
            var user = await _userManager.FindByNameAsync(principal.Data.Identity?.Name);
            if (user is null || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                result.AddError(Errors.InvalidRefreshToken);
                return result;
            }
            var tokenModel = await _jwtService.GenerateAsync(user);
            user.RefreshToken = tokenModel.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.ExpirationRefreshTimeDays);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            result.Data = tokenModel;
            return result;
        }

        public async Task<ApiResult> Revoke(Guid userId, CancellationToken cancellationToken)
        {
            var result = new ApiResult();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                result.AddError(Errors.UserNotFound);
                return result;
            }

            user.RefreshToken = null;
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return result;
        }
    }
}
