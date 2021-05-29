using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<List<Payment>> GetAllPayments();
        public Task<List<Payment>> GetPaymentsByStatus(Enums.PaymentStatus paymentStat);
        public Task<List<Payment>> GetPeriodPayments(DateTime startPeriod, DateTime endPeriod);
        public Task<List<Payment>> GeneratePendingPayments(string currentEmployee);
        public Task<Payment> GetPaymentById(int stateId);
        public Task<Payment> SavePayment(Payment payment);
    }
}
