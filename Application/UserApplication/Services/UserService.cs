using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserApplication.Dto;
using Infrastructure.UnitOfWork;
using MapsterMapper;

namespace Application.UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ClaimViewModel>> GetClaimsByType(Guid userId, string type)
        {
            var userClaim = await _unitOfWork.UserClaimRepository.GetClaimsByType(userId, type);
            return _mapper.Map<List<ClaimViewModel>>(userClaim);
        }
    }
}
