using System;
using System.Threading.Tasks;
using Application.IRepository;
using Domain.IdentityModel;
using Infrastructure.Persistance;

namespace Application.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
