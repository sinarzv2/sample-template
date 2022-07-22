using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities.IdentityModel
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            IsActive = true;
        }

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }

 

  
}
