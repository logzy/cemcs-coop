using AutoMapper;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : Controller
    {
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public BanksController(IBankService bankService, IMapper mapper)
        {

            _bankService = bankService;
            _mapper = mapper;

        }
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _bankService.GetBanks());
        }

    }
}
