using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MemberSavingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberSavingService _memberSavingService;
        private readonly IMapper _mapper;

        public MemberSavingController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMemberSavingService memberSavingService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _memberSavingService = memberSavingService;
            _mapper = mapper;
        }

        // POST: /MemberSavings/All
        [HttpGet("All")]
        public async Task<ActionResult> GetTaskAsync()
        {
            var memSavings = await _memberSavingService.GetAllMemSavings();
            var memSavingsDTO = _mapper.Map<List<MemberSavingDTO>>(memSavings);
            return Ok(memSavingsDTO);

        }

        // GET: MemberSavings/1
        [HttpGet("{memId}")]
        public async Task<ActionResult> GetAsync(int memId)
        {
            var memSaving = await _memberSavingService.GetSavingsByMemId(memId);
            var memSavingDTO = _mapper.Map<MemberSavingDTO>(memSaving);
            return Ok(memSavingDTO);
        }

        // POST: /MemberSavings
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] MemberSavingDTO memSavingDTO)
        {
            if (memSavingDTO.Id == null)
            {
                var mem = _mapper.Map<MemberSaving>(memSavingDTO);
                await _memberSavingService.SaveMemSaving(mem);
            }
            return Ok(memSavingDTO);
        }



    }
}