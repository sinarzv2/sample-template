﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Application.GeneralServices.JwtServices;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Domain.Entities.IdentityModel;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Services
{
    public class AccountService : IAcountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public AccountService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService)
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
                result.AddError(Errors.InvalidUsernameOrPassword);
                return result;
            }

            var isPassValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPassValid)
            {
                result.AddError(Errors.InvalidUsernameOrPassword);
                return result;
            }
            result.Data = await _jwtService.GenerateAsync(user);
            return result;
        }
    }
}