using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    class LoanConfigService : ILoanConfigService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMemberBalanceService _memberBalanceService;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        public LoanConfigService(CoopBankingDataContext context, IMemberBalanceService memberBalanceService, IMapper mapper, IMemberService memberService)
        {
            _context = context;
            _memberBalanceService = memberBalanceService;
            _mapper = mapper;
            _memberService = memberService;
        }

        public Task<LoanConfig> GetLoanConfigById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoanConfig> GetLoanConfigByLoanTypeId(int LoanTypeId)
        {
            var loanType = await _context.LoanConfigs
                .Include(c => c.Loan)
                .FirstOrDefaultAsync(c => (int)c.Loan.LoanType == LoanTypeId);
            return loanType;
        }

        public async Task<LoanConfig> GetLoanConfigByLoanId(int LoanId)
        {
            var loanId = await _context.LoanConfigs
                .Include(c => c.Loan)
                .FirstOrDefaultAsync(c => c.LoanId == LoanId);
            return loanId;
        }

        public async Task<List<LoanConfig>> GetLoanConfigs()
        {
            var configs = await _context.LoanConfigs.ToListAsync();

            return configs;
        }

        public async Task<LoanConfig> SaveLoanConfig(LoanConfig LConfig)
        {
            _context.LoanConfigs.Add(LConfig);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LConfig;
        }
        public async Task<LoanConfig> UpdateLoanConfig(LoanConfig LConfig)
        {
            _context.Attach(LConfig).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LConfig;
        }
        // allvalues.GroupBy(x => x.Id).Select(y=>y.First()).Distinct();
        public async Task<List<LoanConfig>> GetLoanConfigsByMemType(int memType)
        {
            return await _context.LoanConfigs
                .Include(m => m.Loan)
                .Include(m => m.MemberType)
                .Where(m => m.MemberTypeId == memType).Distinct()
                .ToListAsync();

            //     var loanId = await _context.LoanConfigs
            //     .FirstOrDefaultAsync(c => c.LoanId == LoanId);
            // return loanId;

        }

        public async Task<int> GetGuarantorsCount(GuarantorParamsDTO param)
        {
            int count = 0;
            var Employee = await _context.Members
                //.Include(m => m.MemberType)
                .FirstOrDefaultAsync(x => x.Id == param.MemberId);
            if (Employee == null)
                throw new Exception("Employee Not Found");
            var config = await _context.LoanConfigs
                .Include(m => m.Loan)
                .Include(m => m.MemberType)
                .FirstOrDefaultAsync(m => m.MemberTypeId == (int)Employee.MemberType && m.LoanId == param.LoanId);
            if (config != null)
            {
                if (config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                {
                    var GuarantorConfig = await _context.LoanGuarantorConfigs.Where(x => param.LoanAmount < x.GuarantorMaximumAmount && param.LoanAmount >= x.GuarantorMinimumAmount).FirstOrDefaultAsync();
                    //if (GuarantorConfig != null)
                    //{
                    //    count = GuarantorConfig.GuarantorCount;
                    //}
                    if (param.LoanAmount > 0 && param.LoanAmount <= 75000000M)
                    {
                        count = 3;
                    }
                    else if (param.LoanAmount > 75000000M && param.LoanAmount <= 100000000M)
                    {
                        count = 4;
                    }
                    else if (param.LoanAmount > 100000000M && param.LoanAmount <= 125000000M)
                    {
                        count = 5;
                    }
                    else if (param.LoanAmount > 125000000M && param.LoanAmount <= 150000000M)
                    {
                        count = 6;
                    }
                    else if (param.LoanAmount > 150000000M && param.LoanAmount <= 175000000M)
                    {
                        count = 7;
                    }
                    else if (param.LoanAmount > 175000000M && param.LoanAmount <= 200000000M)
                    {
                        count = 8;
                    }
                    else if (param.LoanAmount > 200000000M && param.LoanAmount <= 225000000M)
                    {
                        count = 9;
                    }
                    else if (param.LoanAmount > 225000000M && param.LoanAmount <= 250000000M)
                    {
                        count = 10;
                    }
                    else if (param.LoanAmount > 250000000M && param.LoanAmount <= 275000000M)
                    {
                        count = 11;
                    }
                    else if (param.LoanAmount > 275000000M && param.LoanAmount <= 300000000M)
                    {
                        count = 12;
                    }
                    else if (param.LoanAmount > 300000000M && param.LoanAmount <= 325000000M)
                    {
                        count = 13;
                    }
                    else if (param.LoanAmount > 325000000M && param.LoanAmount <= 350000000M)
                    {
                        count = 14;
                    }
                }
                else
                {
                    if (config.RequiresGuarantors && config.UseYearsOfService)
                    {
                        DateTime EmployementYear = new DateTime(Employee.EmploymentDate.Year, Employee.EmploymentDate.Month, Employee.EmploymentDate.Day);
                        DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        int YearsOfService = DateTime.Now.Year - Employee.EmploymentDate.Year;
                        if (YearsOfService < 10)
                        {
                            if (param.LoanAmount > config.GuarantorMaximumAmount)
                            {
                                count = config.GuarantorCount;
                            }
                        }
                    }
                }
            }

            return count;
        }

        public async Task<LoanConfig> GetLoanConfigByLoanIdAndMember(int LoanId, int memberTypeId)
        {
            var loan = await _context.LoanConfigs
                .Include(c => c.Loan)
                .FirstOrDefaultAsync(c => c.LoanId == LoanId && c.MemberTypeId == memberTypeId);
            return loan;
        }
        public async Task<LoanConfigParamDTO> GetLoanParmsByLoanIdAndMember(int LoanId, int memberTypeId, int MemberId)
        {
            var loan = await _context.LoanConfigs
                .Include(c => c.Loan)
                .FirstOrDefaultAsync(c => c.LoanId == LoanId && c.MemberTypeId == memberTypeId);

            var loangConfig = _mapper.Map<LoanConfigParamDTO>(loan);

            var concurrent = _context.LoanApplications.Where(x => x.MemberId == MemberId && x.LoanId == LoanId && (x.LoanCondition == Enums.LoanConditionStatus.Pending || x.LoanCondition == Enums.LoanConditionStatus.Running)).ToList();
            if (concurrent != null && concurrent.Count > 0)
            {
                loangConfig.ErrorMessage = "You have a running loan";
            }

            return loangConfig;
        }
        public async Task<LoanPlannerDTO> GetLoanPlanner(LoanPlannerParam param, int MemberTypeId, int MemberId)
        {
            LoanPlannerDTO planner = new LoanPlannerDTO();

            var config = await _context.LoanConfigs
               .Include(m => m.Loan)
               //.Include(m => m.Loan.LoanType)
               .Include(m => m.MemberType)
               .FirstOrDefaultAsync(m => m.MemberTypeId == MemberTypeId && m.LoanId == param.loanId);

            if (config != null)
            {
                decimal loanAmount = 1000000M;

                planner.maxRepaymentPeriod = (param.repaymentPeriod > 0) ? param.repaymentPeriod : config.MaxMonthlyRepayPeriod;
                if (param.loanAmount == 0)
                {
                    if (config.MinLoanAmount > 0)
                    {
                        loanAmount = config.MinLoanAmount;
                    }
                    planner.loanAmount = loanAmount;

                }
                else

                {
                    //use amount in the payload for calculation
                    planner.loanAmount = param.loanAmount;
                }
                planner.interestRate = config.IntrestRate;
                if (config.LumpSumSavingsAmount > 0 && config.Loan.LoanType == Enums.LoanType.RegularLoan)
                {
                    var lumpSum = (config.LumpSumSavingsAmount / 100) * loanAmount;
                    planner.RequiredSavings = lumpSum;
                }
                if (config.Loan.LoanType == Enums.LoanType.TargetLoan)
                {
                    var AmountExpected = (param.ExpectedAmount > 0) ? param.ExpectedAmount : loanAmount;
                    var maxLoanAmount = (config.MaxLoanAmount / 100) * AmountExpected;
                    planner.loanAmount = maxLoanAmount;
                }

                var balances = await _memberBalanceService.GetAllMemberBalances(MemberId);
                var SavingsBalance = balances.SavingsBalance + balances.SpecialDepositBalance;
                //Add cash Addition balance here
                if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                {
                    decimal RepaymentAmount = Math.Round((planner.loanAmount) / planner.maxRepaymentPeriod, 2);
                    decimal interest = (((decimal)config.IntrestRate / 100) * planner.loanAmount) / 12;
                    decimal MonthlyRepayment = interest + RepaymentAmount;
                    planner.interest = interest;
                    planner.principal = RepaymentAmount;
                    planner.totalMonthly = MonthlyRepayment;
                }
                else
                {
                    decimal RepaymentAmount = Math.Round(planner.loanAmount, 2);
                    decimal interest = (((decimal)config.IntrestRate / 100) * planner.loanAmount) / 12;
                    decimal MonthlyRepayment = Math.Round(interest + RepaymentAmount, 2);

                    planner.interest = interest;
                    planner.principal = RepaymentAmount;
                    planner.totalMonthly = MonthlyRepayment;
                }
            }
            return planner;
        }
    }
}