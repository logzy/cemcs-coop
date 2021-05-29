using AutoMapper;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IModeOfPaymentService _modesOfPaymentService;
        private readonly IMapper _mapper;
        public PaymentsController(IModeOfPaymentService modesOfPaymentService, IMapper mapper)
        {
            _modesOfPaymentService = modesOfPaymentService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("modes/All")]
        public async Task<IActionResult> GetModesAsync()
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
