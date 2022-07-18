﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities.IdentityModel
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public override int Id { get; set; }
    }
   
}
