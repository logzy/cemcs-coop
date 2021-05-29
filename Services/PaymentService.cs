using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    class PaymentService : IPaymentService
    {
        private readonly CoopBankingDataContext _context;
        public PaymentService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<List<Payment>> GetAllPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return payments;
        }

        public async Task<Payment> GetPaymentById(int stateId)
        {
            var payment = await _context.Payments
                .Where(x => x.Id == stateId)
                .FirstOrDefaultAsync();
            return payment;
        }

        public async Task<List<Payment>> GetPaymentsByStatus(Enums.PaymentStatus paymentStat)
        {
            var payments = await _context.Payments
                .Where(x => x.Status == paymentStat)
                .ToListAsync();
            return payments;
        }
        public async Task<List<Payment>> GeneratePendingPayments(string currentEmployee) 
        {
            var payments = await _context.Payments
                .Where(x => x.Status == Enums.PaymentStatus.Pending)
                .ToListAsync();

            foreach (Payment item in payments)
            {
                item.PaymentDate = DateTime.UtcNow;
                item.PayerEmployeeNumber = currentEmployee;
                item.Status = Enums.PaymentStatus.Paid;
                _context.Attach(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return payments;
        }
        public Task<List<Payment>> GetPeriodPayments(DateTime startPeriod, DateTime endPeriod)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> SavePayment(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
