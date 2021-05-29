using AutoMapper;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuarantorsController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        public GuarantorsController(
          ILoanService loanService,
          IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        [HttpPost("approve")]
        public async Task<ActionResult> PostAsync([FromBody] GuarantorApproverDTO approverDTO)
        {
            var response = await _loanService.ApproveLoanByGuarantor(approverDTO);
            return Ok(response);
        }

    }
}
