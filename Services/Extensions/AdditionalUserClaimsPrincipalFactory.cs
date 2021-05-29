using COOP.Banking.BusinessEntities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Extensions
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            if (UserManager.GetRolesAsync(user).Result.Any())
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "HasRole"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "NoRole"));
            }
            //if (user.Approver == true)
            //    claims.Add(new Claim(JwtClaimTypes.Role, "IsApprover"));
            //if (user.ManageUsers == true)
            //    claims.Add(new Claim(JwtClaimTypes.Role, "IsUserManager"));
            identity.AddClaims(claims);
            return principal;
        }
    }
}
