using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Application.UserApplication.Dto;
using Common.Constant;
using Infrastructure.UnitOfWork;
using MapsterMapper;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<List<ClaimViewModel>> GetClaimsByType(Guid userId, string type)
        {
            var claims = await _distributedCache.GetStringAsync(CacheKeys.ClaimsKey(type, userId));
            if (claims != null)
                return JsonSerializer.Deserialize<List<ClaimViewModel>>(claims);
            
            var userClaim = await _unitOfWork.UserClaimRepository.GetClaimsByType(userId, type);
            var result = _mapper.Map<List<ClaimViewModel>>(userClaim);
            await _distributedCache.SetStringAsync(CacheKeys.ClaimsKey(type, userId),
                JsonSerializer.Serialize(result));
            return result;
        }
    }
}
