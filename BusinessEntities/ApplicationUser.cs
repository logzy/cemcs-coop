using COOP.Banking.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace COOP.Banking.BusinessEntities
{
    public class ApplicationUser : IdentityUser
    {
        public Enums.UserType UserType { get; set; }
        public int UserTypeCategory { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
