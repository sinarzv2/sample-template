using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.IdentityModel
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            IsActive = true;
        }


        public string FullName { get; set; }
        public int Age { get; set; }
  
        public bool IsActive { get; set; }
        
    }

 

  
}
