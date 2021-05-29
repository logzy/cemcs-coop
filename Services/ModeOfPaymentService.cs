using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class ModeOfPaymentService : IModeOfPaymentService
    {
        private readonly CoopBankingDataContext _context;

        public ModeOfPaymentService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<List<ModeOfPayment>> GetModesOfPayment()
        {
            var ModesOfPayment = await _context.ModeOfPayments.Where(x => x.Active).ToListAsync();
            return ModesOfPayment;
        }
    }
}
