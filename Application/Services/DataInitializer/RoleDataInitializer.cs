using System.Linq;
using Domain.Common.Constant;
using Domain.Entities.IdentityModel;
using Infrastructure.IRepository;

namespace Application.Services.DataInitializer
{
    public class RoleDataInitializer : IDataInitializer
    {
        protected  IRoleRepository RoleRepository { get; }

        public RoleDataInitializer(IRoleRepository roleRepository)
        {
            RoleRepository = roleRepository;
        }

        public void InitializeData()
        {
            if (!RoleRepository.TableNoTracking.Any(p => p.Name == ConstantRoles.Admin))
            {
                RoleRepository.Add(new Role()
                {
                    Name = ConstantRoles.Admin,
                    NormalizedName = ConstantRoles.Admin.ToUpper()
                });
            }
            if (!RoleRepository.TableNoTracking.Any(p => p.Name == ConstantRoles.User))
            {
                RoleRepository.Add(new Role()
                {
                    Name = ConstantRoles.User,
                    NormalizedName = ConstantRoles.User.ToUpper()
                });
            }

            RoleRepository.SaveChanges();
        }
    }
}
