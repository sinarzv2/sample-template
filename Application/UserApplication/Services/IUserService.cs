using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserApplication.Dto;
using Common.DependencyLifeTime;

namespace Application.UserApplication.Services
{
    public interface IUserService : IScopedService
    {
        Task<List<ClaimViewModel>> GetClaimsByType(Guid userId, string type);
    }
}
