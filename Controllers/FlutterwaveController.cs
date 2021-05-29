using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlutterwaveController : Controller
    {

        private readonly IFlutterwaveService _flutterService;

        public FlutterwaveController(IFlutterwaveService flutterService)
        {
            _flutterService = flutterService;
        }
        // GET: Department/5
        [HttpPost("account/name")]
        public async Task<ActionResult> GetAsync([FromBody] AccountEnquiryDTO account)
        {
            account.PBFPubKey = "FLWPUBK-0c829d67037c8c431685e19e78d58963-X";
            var name = await _flutterService.GetAccountName(account);
            bool status = (!string.IsNullOrEmpty(name)) ? true : false;
            return Ok(new ServiceResponse<string>()
            {
                Data = name,
                Success = status,
                Message = "Operation successful"
            });
        }
    }
}
