using AutoMapper;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ModesOfPaymentsController : Controller
    {
        private readonly IModeOfPaymentService _modesOfPaymentService;
        private readonly IMapper _mapper;
        public ModesOfPaymentsController(IModeOfPaymentService modesOfPaymentService, IMapper mapper)
        {
            _modesOfPaymentService = modesOfPaymentService;
            _mapper = mapper;
        }

        // GET: /ModesOfPayments/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAllAsync()
        {
            var paymentModes = await _modesOfPaymentService.GetModesOfPayment();
            var mappedPaymentModes = _mapper.Map<List<ModeOfPaymentDTO>>(paymentModes);
            return Ok(new ServiceResponse<List<ModeOfPaymentDTO>>()
            {
                Message = "Operation successufl",
                Data = mappedPaymentModes,
                Success = true
            });
        }
    }
}
