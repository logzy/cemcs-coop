using AutoMapper;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationsController : Controller
    {
        private readonly IMigrationService _migrationService;
        private readonly IMapper _mapper;

        public MigrationsController(IMigrationService migrationService, IMapper mapper)
        {

            _migrationService = migrationService;
            _mapper = mapper;

        }
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            //await _migrationService.MigrateEmployees();
            return Ok();
        }
    }
}
