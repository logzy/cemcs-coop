using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class SavingsConfiguration : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISavingsConfigurationService _savingsConfiguration;

        public SavingsConfiguration(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ISavingsConfigurationService savingsConfiguration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _savingsConfiguration = savingsConfiguration;
        }


        [HttpPost("api/MinSavings")]
        public async Task<IActionResult> AddMinSavings([FromBody] SavingsConfigurationDTO newSavings)
        {
            return Ok(await _savingsConfiguration.CreateSavings(newSavings));
        }

        [HttpGet("api/MinSavings/{savingsId}")]
        public async Task<IActionResult> GetById(int savingsId)
        {
            return Ok(await _savingsConfiguration.GetMinSavingById(savingsId));
        }

        [HttpGet("api/MinSavings/All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _savingsConfiguration.GetMinSavings());
        }
    }
}