using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.IdentityModel
{
    public class Role : IdentityRole<Guid>
    {

        public string Description { get; set; }
    }
}
