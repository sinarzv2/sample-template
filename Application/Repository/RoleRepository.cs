using System.Linq;
using System.Threading.Tasks;
using Application.IRepository;
using Domain.IdentityModel;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}
