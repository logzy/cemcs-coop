using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class BankService : IBankService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;
        public BankService(CoopBankingDataContext context, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<Bank> GetBankByCode(string code)
        {
            var bank = await _context.Banks.FirstOrDefaultAsync(a => a.BankCode == code);
            return bank;
        }
        public async Task<ServiceResponse<List<BankDTO>>> GetBanks()
        {
            ServiceResponse<List<BankDTO>> response = new ServiceResponse<List<BankDTO>>();
            var banks = await _context.Banks.OrderBy(a => a.BankName).ToListAsync();
            if (banks != null && banks.Count > 0)
            {
                response.Data = _mapper.Map<List<BankDTO>>(banks);
                response.Success = true;
                response.Message = "Operation successful";
            }
            else
            {
                response.Success = false;
                response.Message = "Banks not found";
            }
            return response;
        }
    }
}
