using AutoMapper;
//using AutoMapper.Configuration;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace COOP.Banking.Services
{
    public class LoanService : ILoanService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;
        private readonly IMemberSavingService _memberSavingService;
        private readonly IMemberBalanceService _memberBalanceService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IBankService _bankService;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<AppSettings> _appSettings;
        //private readonly IPendingApprovalService<ModuleApprover> _pendingApprovalService;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IConfiguration _configuration;
        private string[] permittedExtensions = { ".png", ".pdf", ".jpg", ".jpeg", ".gif" };
        public LoanService(CoopBankingDataContext context, IMapper mapper, IMemberSavingService memberSavingService, IMemberBalanceService memberBalanceService, IEmailSender emailSender,
            IModuleApproverService moduleApproverService,
            IBeneficiaryService beneficiaryService,
            IBankService bankService,
            //IPendingApprovalService<ModuleApprover> pendingApprovalService, 
            IOptions<AppSettings> appSettings,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _memberSavingService = memberSavingService;
            _memberBalanceService = memberBalanceService;
            _emailSender = emailSender;
            _moduleApproverService = moduleApproverService;
            //_pendingApprovalService = pendingApprovalService;
            _appSettings = appSettings;
            _beneficiaryService = beneficiaryService;
            _bankService = bankService;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> ApproveLoanByGuarantor(GuarantorApproverDTO approver)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var Guarantor = await _context.LoanGuarantors
                .Include(l => l.LoanApplication)
                .Include(l => l.LoanApplication.Member)
                .Include(l => l.LoanApplication.Member.Person)
                .FirstOrDefaultAsync(x => x.LoanApplicationId == approver.LoanApplicationId && x.EmployeeNumber == approver.EmployeeNumber);

            // Get module approver for loans
            Module module = await _moduleApproverService.GetModuleByNormalizedName("LOANS");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);

            if (Guarantor != null)
            {
                if (Guarantor.ApprovalStatus == Enums.ApprovalStatus.Approved)
                {
                    response.Data = "You have already approved this request";
                }
                else if (Guarantor.ApprovalStatus == Enums.ApprovalStatus.Rejected)
                {
                    response.Data = "You have already rejected this request";
                }
                else
                {
                    var CurrentApprovalCount = Guarantor.LoanApplication.GuarantorApprovalCount;
                    var finalApproval = false;
                    if (approver.Status == (int)Enums.ApprovalStatus.Approved)
                    {
                        if (Guarantor.LoanApplication.GuarantorApprovalCount < Guarantor.LoanApplication.RequiredGuarantorsCount)
                        {
                            CurrentApprovalCount = Guarantor.LoanApplication.GuarantorApprovalCount + 1;
                            Guarantor.ApprovalStatus = Enums.ApprovalStatus.Approved;
                            Guarantor.ApprovalDate = DateTime.Now;
                            Guarantor.Comments = approver.Comments;
                            Guarantor.LoanApplication.GuarantorApprovalCount = CurrentApprovalCount;

                            if (CurrentApprovalCount == Guarantor.LoanApplication.RequiredGuarantorsCount)
                            {
                                finalApproval = true;
                            }
                        }
                        else
                        {
                            response.Data = "Guarantor approval complete";
                        }
                    }
                    else
                    {
                        Guarantor.ApprovalStatus = Enums.ApprovalStatus.Rejected;
                        Guarantor.ApprovalDate = DateTime.Now;
                        Guarantor.Comments = approver.Comments;
                    }
                    _context.LoanGuarantors.Update(Guarantor);
                    await _context.SaveChangesAsync();
                    StringBuilder builder = new StringBuilder();
                    string Title = string.Empty;

                    builder.AppendLine(string.Format("<p>Dear {0}, </p>", Guarantor.LoanApplication.Member.Person.FirstName));
                    if (approver.Status == (int)Enums.ApprovalStatus.Approved)
                    {

                        if (finalApproval)
                        {
                            Title = "Final Guarantor Approval";
                            builder.AppendLine(string.Format("<p>{0} has approved your guarantor request.</p>", Guarantor.GuarantorName));
                            builder.AppendLine(string.Format("<p>Your guarantor approval is complete.</p>"));
                            // Enter into the pending table
                            await SubmitForApproval(moduleApprover, Guarantor.LoanApplicationId);
                        }
                        else
                        {
                            Title = "Guarantor Approval";
                            builder.AppendLine(string.Format("<p>{0} has approved your guarantor request.</p>", Guarantor.GuarantorName));
                        }
                    }
                    else
                    {
                        builder.AppendLine(string.Format("<p>{0} has declined to be your guarantor for the loan details below </p>", Guarantor.GuarantorName));
                    }

                    builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", Guarantor.LoanApplication.LoanAmount)));
                    response.Data = "Successful";
                    try
                    {
                        await _emailSender.SendEmailAsync(Guarantor.LoanApplication.Member.Person.Email, Title, builder.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            else
            {
                response.Data = "No record found";
            }

            response.Success = true;
            response.Message = "Operation Successful";
            return response;
        }

        public async Task<ServiceResponse<Loan>> CreateLoan(LoanDTO newloan)
        {
            ServiceResponse<Loan> response = new ServiceResponse<Loan>();

            Loan loan = _mapper.Map<Loan>(newloan);

            if (await _context.Loans
                    .AnyAsync(c => c.LoanCode.ToLower() == newloan.LoanCode.ToLower()))
            {
                response.Success = false;
                response.Message = "Loan Already Exist";
                return response;
            }

            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<Loan>(loan);
            return response;
        }

        public async Task<ServiceResponse<LoanApplicationDTO>> GetLoanById(int id)
        {
            ServiceResponse<LoanApplicationDTO> response = new ServiceResponse<LoanApplicationDTO>();
            LoanApplication applyLoanDTO = await _context.LoanApplications
            .Where(c => c.Id == id).Include(m => m.Member)
            .ThenInclude(p => p.Person)
            .FirstOrDefaultAsync();
            response.Data = _mapper.Map<LoanApplicationDTO>(applyLoanDTO);
            response.Success = true;
            return response;
        }

        public async Task<LoanApplication> GetLoanByTag(string tag)
        {
            LoanApplication applyLoanDTO = await _context.LoanApplications
                .Where(c => c.Tag == tag)
                .FirstOrDefaultAsync();
            return applyLoanDTO;
        }
        public async Task<LoanApplication> GetLoanDataById(int id)
        {
            LoanApplication applyLoanDTO = await _context.LoanApplications
                .Where(c => c.Id == id).Include(m => m.Member)
                .ThenInclude(p => p.Person)
                .FirstOrDefaultAsync();
            return applyLoanDTO;
        }
        public async Task<ServiceResponse<List<LoanApplication>>> GetPaidLoanApplications()
        {
            ServiceResponse<List<LoanApplication>> response = new ServiceResponse<List<LoanApplication>>();
            List<LoanApplication> applyLoan = await _context.LoanApplications
                .Where(c => c.Approved && c.IsPaid).Include(m => m.Member)
                .ThenInclude(p => p.Person)
                .ToListAsync();
            response.Data = applyLoan;
            response.Success = true;
            return response;
        }
        public async Task<ServiceResponse<List<LoanApplication>>> GetUnpaidLoanApplications()
        {
            ServiceResponse<List<LoanApplication>> response = new ServiceResponse<List<LoanApplication>>();
            List<LoanApplication> applyLoan = await _context.LoanApplications
                .Where(c => c.Approved && !c.IsPaid).Include(m => m.Member)
                .ThenInclude(p => p.Person)
                .ToListAsync();
            response.Data = applyLoan;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<LoanDTO>>> GetLoans()
        {
            ServiceResponse<List<LoanDTO>> response = new ServiceResponse<List<LoanDTO>>();
            List<Loan> mLoan = await _context.Loans.ToListAsync();
            response.Data = (mLoan.Select(c => _mapper.Map<LoanDTO>(c))).ToList();
            return response;
        }
        public async Task<ServiceResponse<List<RepaymentDTO>>> SaveMimeLoanApplication(MimeLoanPostDTO loanData)
        {
            var AppUrl = _configuration["AppSettings:SiteInfo:Url"];
            // Get module approver for loans
            Module module = await _moduleApproverService.GetModuleByNormalizedName("LOANS");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
                return new ServiceResponse<List<RepaymentDTO>>()
                {
                    Data = null,
                    Success = false,
                    Message = "NO REGISTERED MODULE APPROVER FOR LOANS"
                };

            var validation = await this.ValidateMimeLoanApplication(loanData);
            List<RepaymentDTO> repayments = new List<RepaymentDTO>();
            List<Error> errors = new List<Error>();
            if (validation.Status)
            {
                return new ServiceResponse<List<RepaymentDTO>>()
                {
                    Data = null,
                    Success = false,
                    Message = "Operation failed",
                    Errors = validation.Errors
                };
            }
            else
            {
                var member = await _context.Members
                        .Include(s => s.Person)
                       .FirstOrDefaultAsync(x => x.Id == loanData.MemberId);
                LoanConfig config = await _context.LoanConfigs.Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                        .Include(s => s.Loan)
                        .Include(s => s.MemberType)
                        .FirstOrDefaultAsync();

                if (config == null)
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                    error.ErrorMessage = "Loan not configured";
                    errors.Add(error);
                    return new ServiceResponse<List<RepaymentDTO>>()
                    {
                        Data = null,
                        Success = false,
                        Message = "Operation failed",
                        Errors = errors
                    };
                }
                else
                {

                    if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                    {
                        decimal RepaymentAmount = Math.Round((loanData.LoanAmount) / loanData.RepaymntPeriod, 2);
                        decimal interest = (((decimal)loanData.InterestRate / 100) * loanData.LoanAmount) / 12;
                        decimal MonthlyRepayment = interest + RepaymentAmount;

                        int EffectiveMonth = loanData.EffectiveMonth;
                        int EffectiveYear = loanData.EffectiveYear;
                        if (loanData.EffectiveMonth == 12)
                        {
                            EffectiveYear = loanData.EffectiveYear + 1;
                            EffectiveMonth = 1;
                        }


                        DateTime LoanEffectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 15);
                        DateTime LastRepaymentDate = LoanEffectiveDate;


                        var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);

                        var bank = await _bankService.GetBankByCode(loanData.BankCode);
                        LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                        loan.Interest = interest;
                        loan.RepaymentPeriod = loanData.RepaymntPeriod;
                        loan.Principal = RepaymentAmount;
                        loan.EffectiveDate = LoanEffectiveDate;
                        loan.MethodOfCollectionId = 2;
                        loan.DateSubmitted = DateTime.UtcNow;
                        if (bank != null)
                        {
                            loan.BankName = bank.BankName;
                        }
                        loan.AccountName = loanData.AccountName;
                        loan.AccountNumber = loanData.AccountNumber;
                        loan.BankCode = loanData.BankCode;
                        if (loanData.FormFile.Length > 0)
                        {

                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Retiree/Payslips", loanData.FormFile.FileName);
                            loan.filePath = path;

                            using (var stream = System.IO.File.Create(path))
                            {
                                await loanData.FormFile.CopyToAsync(stream);
                            }
                        }
                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            loan.LoanGuarantors = loanGuarantors;
                            loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                        }
                        await _context.LoanApplications.AddAsync(loan);
                        await _context.SaveChangesAsync();
                        for (int counter = 1; counter <= loanData.RepaymntPeriod; counter++)
                        {
                            var nextRepaymentDate = LastRepaymentDate.AddMonths(counter);
                            RepaymentDTO repayment = new RepaymentDTO();
                            repayment.Principal = RepaymentAmount;
                            repayment.Interest = interest;
                            repayment.RepaymentDate = nextRepaymentDate;
                            repayment.TotalPayment = MonthlyRepayment;
                            repayment.LoanApplicationId = loan.Id;

                            repayments.Add(repayment);

                        }

                        await _context.LoanRepayments.AddRangeAsync(_mapper.Map<List<LoanRepayment>>(repayments));
                        Beneficiary beneficiary = new Beneficiary();
                        beneficiary.MemberId = loanData.MemberId;
                        beneficiary.AccountName = loanData.AccountName;
                        beneficiary.AccountNumber = loanData.AccountNumber;

                        // var bank = _bankService.GetBankByCode(loanData.BankCode);
                        if (bank != null)
                        {
                            beneficiary.BankId = bank.Id;

                            var existingAccount = await _context.Beneficiaries.FirstOrDefaultAsync(x => x.AccountName == beneficiary.AccountName && x.BankId == beneficiary.BankId && x.MemberId == beneficiary.MemberId);
                            if (existingAccount == null)
                            {
                                _context.Beneficiaries.Add(beneficiary);
                                await _context.SaveChangesAsync();
                            }
                        }
                        //Add Beneficiaries
                        await _context.SaveChangesAsync();
                        // await _beneficiaryService.AddLoanBeneficary(beneficiary.Id, loan.Id);

                        string Title = string.Format("{0} Loan Application", config.Loan.Name);
                        StringBuilder mailSb = new StringBuilder();

                        mailSb.AppendLine(string.Format("Your {0} Loan Application has been submitted.</br>", config.Loan.Name));
                        mailSb.AppendLine("<p>It is presently undergoing processing.</p>");
                        mailSb.AppendLine(String.Format("<p>Your Bank for this application: {0}</p>", bank.BankName));
                        mailSb.AppendLine(String.Format("<p>Your Bank Account No. for this application:  {0}</p>", loanData.AccountNumber));
                        mailSb.AppendLine("You will receive emails as soon as the required endorsements are done.</br>");

                        try
                        {
                            await _emailSender.SendEmailAsync(member.Person.Email, Title, mailSb.ToString());
                        }
                        catch (Exception)
                        {
                        }

                        StringBuilder approverlSb = new StringBuilder();

                        approverlSb.AppendLine(string.Format("A {0} Loan Application of an employee has been submitted.</br>", config.Loan.Name));
                        approverlSb.AppendLine("It is presently undergoing processing.</br>");
                        approverlSb.AppendLine(String.Format("Employee No : {0}</br>", member.EmployeeNumber));
                        approverlSb.AppendLine(String.Format("Name of employee :  {0} {1}</br>", member.Person.FirstName, member.Person.LastName));
                        approverlSb.AppendLine("You are therefore required to approve the application.</br>");

                        // mailSb.AppendLine("genSecName </br>");
                        //mailSb.AppendLine("Asst. General Secretary - CEMCS.</br>");

                        // mailSb.AppendLine("This is the link to the home page");
                        //  mailSb.AppendLine("<a href='" + AppUrl + "'>");


                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            foreach (var guarantor in loanData.LoanGuarantors)
                            {
                                StringBuilder builder = new StringBuilder();

                                var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));


                                //var callbackUrl = "http://localhost:8080/guarantor?code=" + encodedParams;
                                var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                //Send email
                                string message = $"";
                                builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));

                                builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));

                                try
                                {
                                    await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                        else
                        {
                            //send mail for first line approver

                            //Get inserted loan by tag
                            //LoanApplication addedloanApplication = await GetLoanByTag(loan.Tag);
                            // Enter into the pending table
                            await SubmitForApproval(moduleApprover, loan.Id);

                        }


                        return new ServiceResponse<List<RepaymentDTO>>
                        {
                            Data = repayments,
                            Success = true,
                            Message = "Operation successful",
                            Errors = null
                        };
                    }
                    else
                    {
                        decimal RepaymentAmount = Math.Round(loanData.LoanAmount, 2);
                        decimal interest = (((decimal)config.IntrestRate / 100) * loanData.LoanAmount) / 12;
                        decimal MonthlyRepayment = Math.Round(interest + RepaymentAmount, 2);

                        DateTime LastRepaymentDate = new DateTime(loanData.EffectiveYear, loanData.EffectiveMonth, 1);

                        RepaymentDTO repayment = new RepaymentDTO();
                        repayment.Principal = RepaymentAmount;
                        repayment.Interest = interest;
                        repayment.Id = 1;
                        repayment.RepaymentDate = LastRepaymentDate;
                        repayment.TotalPayment = MonthlyRepayment;
                        repayments.Add(repayment);

                        var bank = await _bankService.GetBankByCode(loanData.BankCode);


                        var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);
                        List<LoanGuarantor> LoanGuarantors = new List<LoanGuarantor>();
                        LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                        loan.Interest = interest;
                        loan.RepaymentPeriod = loanData.RepaymntPeriod;
                        loan.Principal = RepaymentAmount;
                        loan.EffectiveDate = LastRepaymentDate;
                        loan.MethodOfCollectionId = 2;
                        loan.DateSubmitted = DateTime.UtcNow;
                        if (loanData.FormFile.Length > 0)
                        {
                            //var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", Path.GetRandomFileName());
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Retiree/Payslips", Path.GetRandomFileName());
                            loan.filePath = path;
                        }
                        if (loanData.LoanGuarantors.Count > 0)
                        {
                            loan.LoanGuarantors = loanGuarantors;
                            loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                        }

                        await _context.LoanApplications.AddAsync(loan);
                        Beneficiary beneficiary = new Beneficiary();
                        beneficiary.MemberId = loanData.MemberId;
                        beneficiary.AccountName = loanData.AccountName;
                        beneficiary.AccountNumber = loanData.AccountNumber;


                        if (bank == null)
                        {
                            beneficiary.BankId = bank.Id;
                            var existingAccount = await _context.Beneficiaries.FirstOrDefaultAsync(x => x.AccountName == beneficiary.AccountName && x.BankId == beneficiary.BankId && x.MemberId == beneficiary.MemberId);
                            if (existingAccount != null)
                            {
                                _context.Beneficiaries.Add(beneficiary);
                                await _context.SaveChangesAsync();
                            }
                        }
                        //await _beneficiaryService.AddLoanBeneficary(beneficiary.Id, loan.Id);
                        //Add Beneficiaries
                        await _context.SaveChangesAsync();

                        string Title = string.Format("{0} Loan Application", config.Loan.Name);
                        StringBuilder mailSb = new StringBuilder();

                        mailSb.AppendLine(string.Format("Your {0} Loan Application has been submitted.</br>", config.Loan.Name));
                        mailSb.AppendLine("It is presently undergoing processing.</br>");
                        mailSb.AppendLine(String.Format("Your Bank for this application: {0}</br>", bank.BankName));
                        mailSb.AppendLine(String.Format("Your Bank Account No. for this application:  {0}</br>", loanData.AccountNumber));
                        mailSb.AppendLine("You will receive emails as soon as the required endorsements are done.</br>");

                        try
                        {
                            await _emailSender.SendEmailAsync(member.Person.Email, Title, mailSb.ToString());
                        }
                        catch (Exception)
                        {
                        }

                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            foreach (var guarantor in loanData.LoanGuarantors)
                            {
                                StringBuilder builder = new StringBuilder();
                                var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));

                                //var callbackUrl = "http://localhost:8080/guarantor?code=" + encodedParams;
                                var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));
                                builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));
                                try
                                {
                                    await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                        else
                        {
                            //Get inserted loan by tag
                            LoanApplication addedloanApplication = await GetLoanByTag(loan.Tag);
                            // Enter into the pending table
                            await SubmitForApproval(moduleApprover, addedloanApplication.Id);
                        }
                        return new ServiceResponse<List<RepaymentDTO>>
                        {
                            Data = null,
                            Success = true,
                            Message = "Operation successful",
                            Errors = null
                        };
                    }
                }


            }
        }
        public async Task<ServiceResponse<List<RepaymentDTO>>> SaveLoanApplication(LoanPostDTO loanData)
        {
            var AppUrl = _configuration["AppSettings:SiteInfo:Url"];
            // Get module approver for loans
            Module module = await _moduleApproverService.GetModuleByNormalizedName("LOANS");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
                return new ServiceResponse<List<RepaymentDTO>>()
                {
                    Data = null,
                    Success = false,
                    Message = "NO REGISTERED MODULE APPROVER FOR LOANS"
                };

            var validation = await this.ValidateLoanApplication(loanData);
            List<RepaymentDTO> repayments = new List<RepaymentDTO>();
            List<Error> errors = new List<Error>();
            if (validation.Status)
            {
                return new ServiceResponse<List<RepaymentDTO>>()
                {
                    Data = null,
                    Success = false,
                    Message = "Operation failed",
                    Errors = validation.Errors
                };
            }
            else
            {
                var member = await _context.Members
                        .Include(s => s.Person)
                       .FirstOrDefaultAsync(x => x.Id == loanData.MemberId);
                LoanConfig config = await _context.LoanConfigs.Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                        .Include(s => s.Loan)
                        .Include(s => s.MemberType)
                        .FirstOrDefaultAsync();

                if (config == null)
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                    error.ErrorMessage = "Loan not configured";
                    errors.Add(error);
                    return new ServiceResponse<List<RepaymentDTO>>()
                    {
                        Data = null,
                        Success = false,
                        Message = "Operation failed",
                        Errors = errors
                    };
                }
                else
                {
                    if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                    {
                        decimal RepaymentAmount = Math.Round((loanData.LoanAmount) / loanData.RepaymntPeriod, 2);
                        decimal interest = (((decimal)loanData.InterestRate / 100) * loanData.LoanAmount) / 12;
                        decimal MonthlyRepayment = interest + RepaymentAmount;

                        int EffectiveMonth = loanData.EffectiveMonth;
                        int EffectiveYear = loanData.EffectiveYear;
                        if (loanData.EffectiveMonth == 12)
                        {
                            EffectiveYear = loanData.EffectiveYear + 1;
                            EffectiveMonth = 1;
                        }


                        DateTime LoanEffectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 15);
                        DateTime LastRepaymentDate = LoanEffectiveDate;


                        var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);

                        var bank = await _bankService.GetBankByCode(loanData.BankCode);
                        LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                        loan.Interest = interest;
                        loan.RepaymentPeriod = loanData.RepaymntPeriod;
                        loan.Principal = RepaymentAmount;
                        loan.EffectiveDate = LoanEffectiveDate;
                        loan.MethodOfCollectionId = 2;
                        loan.DateSubmitted = DateTime.UtcNow;
                        if (bank != null)
                        {
                            loan.BankName = bank.BankName;
                        }
                        loan.AccountName = loanData.AccountName;
                        loan.AccountNumber = loanData.AccountNumber;
                        loan.BankCode = loanData.BankCode;
                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            loan.LoanGuarantors = loanGuarantors;
                            loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                        }
                        await _context.LoanApplications.AddAsync(loan);
                        await _context.SaveChangesAsync();
                        for (int counter = 1; counter <= loanData.RepaymntPeriod; counter++)
                        {
                            var nextRepaymentDate = LastRepaymentDate.AddMonths(counter);
                            RepaymentDTO repayment = new RepaymentDTO();
                            repayment.Principal = RepaymentAmount;
                            repayment.Interest = interest;
                            repayment.RepaymentDate = nextRepaymentDate;
                            repayment.TotalPayment = MonthlyRepayment;
                            repayment.LoanApplicationId = loan.Id;

                            repayments.Add(repayment);

                        }

                        await _context.LoanRepayments.AddRangeAsync(_mapper.Map<List<LoanRepayment>>(repayments));
                        Beneficiary beneficiary = new Beneficiary();
                        beneficiary.MemberId = loanData.MemberId;
                        beneficiary.AccountName = loanData.AccountName;
                        beneficiary.AccountNumber = loanData.AccountNumber;

                        // var bank = _bankService.GetBankByCode(loanData.BankCode);
                        if (bank != null)
                        {
                            beneficiary.BankId = bank.Id;

                            var existingAccount = await _context.Beneficiaries.FirstOrDefaultAsync(x => x.AccountName == beneficiary.AccountName && x.BankId == beneficiary.BankId && x.MemberId == beneficiary.MemberId);
                            if (existingAccount == null)
                            {
                                _context.Beneficiaries.Add(beneficiary);
                                await _context.SaveChangesAsync();
                            }
                        }
                        //Add Beneficiaries
                        await _context.SaveChangesAsync();
                        // await _beneficiaryService.AddLoanBeneficary(beneficiary.Id, loan.Id);

                        string Title = string.Format("{0} Loan Application", config.Loan.Name);
                        StringBuilder mailSb = new StringBuilder();

                        mailSb.AppendLine(string.Format("Your {0} Loan Application has been submitted.</br>", config.Loan.Name));
                        mailSb.AppendLine("<p>It is presently undergoing processing.</p>");
                        mailSb.AppendLine(String.Format("<p>Your Bank for this application: {0}</p>", bank.BankName));
                        mailSb.AppendLine(String.Format("<p>Your Bank Account No. for this application:  {0}</p>", loanData.AccountNumber));
                        mailSb.AppendLine("You will receive emails as soon as the required endorsements are done.</br>");

                        try
                        {
                            await _emailSender.SendEmailAsync(member.Person.Email, Title, mailSb.ToString());
                        }
                        catch (Exception)
                        {
                        }

                        StringBuilder approverlSb = new StringBuilder();

                        approverlSb.AppendLine(string.Format("A {0} Loan Application of an employee has been submitted.</br>", config.Loan.Name));
                        approverlSb.AppendLine("It is presently undergoing processing.</br>");
                        approverlSb.AppendLine(String.Format("Employee No : {0}</br>", member.EmployeeNumber));
                        approverlSb.AppendLine(String.Format("Name of employee :  {0} {1}</br>", member.Person.FirstName, member.Person.LastName));
                        approverlSb.AppendLine("You are therefore required to approve the application.</br>");

                        // mailSb.AppendLine("genSecName </br>");
                        //mailSb.AppendLine("Asst. General Secretary - CEMCS.</br>");

                        // mailSb.AppendLine("This is the link to the home page");
                        //  mailSb.AppendLine("<a href='" + AppUrl + "'>");


                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            foreach (var guarantor in loanData.LoanGuarantors)
                            {
                                StringBuilder builder = new StringBuilder();

                                var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));


                                //var callbackUrl = "http://localhost:8080/guarantor?code=" + encodedParams;
                                var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                //Send email
                                string message = $"";
                                builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));

                                builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));

                                try
                                {
                                    await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                        else
                        {
                            //send mail for first line approver

                            //Get inserted loan by tag
                            //LoanApplication addedloanApplication = await GetLoanByTag(loan.Tag);
                            // Enter into the pending table
                            await SubmitForApproval(moduleApprover, loan.Id);

                        }


                        return new ServiceResponse<List<RepaymentDTO>>
                        {
                            Data = repayments,
                            Success = true,
                            Message = "Operation successful",
                            Errors = null
                        };
                    }
                    else
                    {
                        decimal RepaymentAmount = Math.Round(loanData.LoanAmount, 2);
                        decimal interest = (((decimal)config.IntrestRate / 100) * loanData.LoanAmount) / 12;
                        decimal MonthlyRepayment = Math.Round(interest + RepaymentAmount, 2);

                        DateTime LastRepaymentDate = new DateTime(loanData.EffectiveYear, loanData.EffectiveMonth, 1);

                        RepaymentDTO repayment = new RepaymentDTO();
                        repayment.Principal = RepaymentAmount;
                        repayment.Interest = interest;
                        repayment.Id = 1;
                        repayment.RepaymentDate = LastRepaymentDate;
                        repayment.TotalPayment = MonthlyRepayment;
                        repayments.Add(repayment);

                        var bank = await _bankService.GetBankByCode(loanData.BankCode);


                        var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);
                        List<LoanGuarantor> LoanGuarantors = new List<LoanGuarantor>();
                        LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                        loan.Interest = interest;
                        loan.RepaymentPeriod = loanData.RepaymntPeriod;
                        loan.Principal = RepaymentAmount;
                        loan.EffectiveDate = LastRepaymentDate;
                        loan.MethodOfCollectionId = 2;
                        loan.DateSubmitted = DateTime.UtcNow;

                        if (loanData.LoanGuarantors.Count > 0)
                        {
                            loan.LoanGuarantors = loanGuarantors;
                            loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                        }

                        await _context.LoanApplications.AddAsync(loan);
                        Beneficiary beneficiary = new Beneficiary();
                        beneficiary.MemberId = loanData.MemberId;
                        beneficiary.AccountName = loanData.AccountName;
                        beneficiary.AccountNumber = loanData.AccountNumber;


                        if (bank == null)
                        {
                            beneficiary.BankId = bank.Id;
                            var existingAccount = await _context.Beneficiaries.FirstOrDefaultAsync(x => x.AccountName == beneficiary.AccountName && x.BankId == beneficiary.BankId && x.MemberId == beneficiary.MemberId);
                            if (existingAccount != null)
                            {
                                _context.Beneficiaries.Add(beneficiary);
                                await _context.SaveChangesAsync();
                            }
                        }
                        //await _beneficiaryService.AddLoanBeneficary(beneficiary.Id, loan.Id);
                        //Add Beneficiaries
                        await _context.SaveChangesAsync();

                        string Title = string.Format("{0} Loan Application", config.Loan.Name);
                        StringBuilder mailSb = new StringBuilder();

                        mailSb.AppendLine(string.Format("Your {0} Loan Application has been submitted.</br>", config.Loan.Name));
                        mailSb.AppendLine("It is presently undergoing processing.</br>");
                        mailSb.AppendLine(String.Format("Your Bank for this application: {0}</br>", bank.BankName));
                        mailSb.AppendLine(String.Format("Your Bank Account No. for this application:  {0}</br>", loanData.AccountNumber));
                        mailSb.AppendLine("You will receive emails as soon as the required endorsements are done.</br>");

                        try
                        {
                            await _emailSender.SendEmailAsync(member.Person.Email, Title, mailSb.ToString());
                        }
                        catch (Exception)
                        {
                        }

                        if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                        {
                            foreach (var guarantor in loanData.LoanGuarantors)
                            {
                                StringBuilder builder = new StringBuilder();
                                var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));

                                //var callbackUrl = "http://localhost:8080/guarantor?code=" + encodedParams;
                                var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));
                                builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));
                                try
                                {
                                    await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                        else
                        {
                            //Get inserted loan by tag
                            LoanApplication addedloanApplication = await GetLoanByTag(loan.Tag);
                            // Enter into the pending table
                            await SubmitForApproval(moduleApprover, addedloanApplication.Id);
                        }
                        return new ServiceResponse<List<RepaymentDTO>>
                        {
                            Data = null,
                            Success = true,
                            Message = "Operation successful",
                            Errors = null
                        };
                    }
                }


            }
        }
        public async Task<ServiceResponse<List<RepaymentDTO>>> CreateLoanApplication(LoanPostDTO loanData)
        {
            List<Error> errors = new List<Error>();
            List<RepaymentDTO> repayments = new List<RepaymentDTO>();
            try
            {
                if ((int)loanData.FormAction == (int)Enums.FormAction.Validate)
                {
                    var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == loanData.MemberId);
                    if (member == null)
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.ItemNotFound.ToString();
                        error.ErrorMessage = "Invalid Member Supplied";
                        errors.Add(error);
                        return new ServiceResponse<List<RepaymentDTO>>()
                        {
                            Data = null,
                            Success = false,
                            Message = "Operation failed",
                            Errors = errors
                        };
                    }
                    else
                    {
                        var ValidationFailed = false;
                        var config = await _context.LoanConfigs.Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                            .Include(s => s.Loan)
                            .Include(s => s.MemberType)
                            .FirstOrDefaultAsync();
                        if (config == null)
                        {
                            Error error = new Error();
                            error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                            error.ErrorMessage = "Loan not configured";
                            errors.Add(error);
                            return new ServiceResponse<List<RepaymentDTO>>()
                            {
                                Data = null,
                                Success = false,
                                Message = "Operation failed",
                                Errors = errors
                            };
                        }
                        else
                        {

                            //   var Savings = await _memberSavingService.GetSavingsByMemId(loanData.MemberId);
                            decimal balance = 0;
                            balance = await _memberBalanceService.GetMeberSavingsBalances((int)Enums.SavingsType.savings, Enums.BalanceType.MemberSavings, loanData.MemberId);
                            //if (Savings.Count > 0)
                            //{
                            //    var SavingsBalance = Savings.FirstOrDefault(x => x.Type == (int)Enums.SavingsType.savings);
                            //    balance = await _memberBalanceService.GetMeberSavingsBalances(SavingsBalance.Id, Enums.BalanceType.MemberSavings, loanData.MemberId);
                            //}
                            if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                            {
                                decimal MinAmount = config.MinLoanAmount;
                                decimal MaxLoanAmount = config.MaxLoanAmount;
                                int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                                int MaximumRepaymentPeriod = config.MaxMonthlyRepayPeriod;
                                decimal lumpSumSavings = config.LumpSumSavingsAmount;

                                if (MinAmount != 0 && loanData.LoanAmount < MinAmount)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Loan Amount Below Minimum Amount",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                                if (MaxLoanAmount != 0 && loanData.LoanAmount > MaxLoanAmount)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Loan Amount Above Maximum Amount",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                                if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod < MinimumRepaymentReriod)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Repayment Period Less than Minimum Repayment Period",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                                if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod > MinimumRepaymentReriod)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Repayment Period more than Maximum Repayment Period",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                                if (lumpSumSavings > 0)
                                {
                                    decimal savingParam = (lumpSumSavings / 100) * loanData.LoanAmount;
                                    if (balance < savingParam)
                                    {
                                        var error = new Error()
                                        {
                                            ErrorMessage = "Not enough savings to apply for loan",
                                            ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                        };
                                        errors.Add(error);
                                        ValidationFailed = true;
                                    }
                                }

                            }
                            if (config.Loan.LoanType == Enums.LoanType.TargetLoan)
                            {


                                decimal MaxLoanAmount = config.MaxLoanAmount;
                                int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                                decimal allowedAmount = (MaxLoanAmount / 100) * loanData.ExpectedAmount;

                                if (loanData.LoanAmount > allowedAmount)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Loan Amount Above Maximum Amount",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }



                            }


                            if (!ValidationFailed)
                            {
                                if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                                {
                                    decimal RepaymentAmount = Math.Round((loanData.LoanAmount) / loanData.RepaymntPeriod, 2);
                                    decimal interest = (((decimal)loanData.InterestRate / 100) * loanData.LoanAmount) / 12;
                                    decimal MonthlyRepayment = interest + RepaymentAmount;

                                    int EffectiveMonth = loanData.EffectiveMonth;
                                    int EffectiveYear = loanData.EffectiveYear;
                                    if (loanData.EffectiveMonth == 12)
                                    {
                                        EffectiveYear = loanData.EffectiveYear + 1;
                                        EffectiveMonth = 1;
                                    }


                                    DateTime LoanEffectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 15);
                                    DateTime LastRepaymentDate = LoanEffectiveDate;
                                    for (int counter = 1; counter <= loanData.RepaymntPeriod; counter++)
                                    {
                                        RepaymentDTO repayment = new RepaymentDTO();
                                        repayment.Principal = RepaymentAmount;
                                        repayment.Interest = interest;
                                        repayment.Id = counter;
                                        repayment.RepaymentDate = LastRepaymentDate;
                                        repayment.TotalPayment = MonthlyRepayment;
                                        LastRepaymentDate.AddMonths(1);
                                        repayments.Add(repayment);

                                    }
                                    return new ServiceResponse<List<RepaymentDTO>>
                                    {
                                        Data = repayments,
                                        Success = true,
                                        Message = "Operation successful",
                                        Errors = null
                                    };
                                }
                                else
                                {
                                    decimal RepaymentAmount = Math.Round(loanData.LoanAmount, 2);
                                    decimal interest = (((decimal)config.IntrestRate / 100) * loanData.LoanAmount) / 12;
                                    decimal MonthlyRepayment = Math.Round(interest + RepaymentAmount, 2);

                                    DateTime LastRepaymentDate = new DateTime(loanData.EffectiveYear, loanData.EffectiveMonth, 1);

                                    RepaymentDTO repayment = new RepaymentDTO();
                                    repayment.Principal = RepaymentAmount;
                                    repayment.Interest = interest;
                                    repayment.Id = 1;
                                    repayment.RepaymentDate = LastRepaymentDate;
                                    repayment.TotalPayment = MonthlyRepayment;
                                    repayments.Add(repayment);

                                    return new ServiceResponse<List<RepaymentDTO>>
                                    {
                                        Data = repayments,
                                        Success = true,
                                        Message = "Operation successful",
                                        Errors = null
                                    };
                                }



                                //inetesrt= 
                                //intrate/numberOfrepayments*principal
                            }
                            else
                            {
                                return new ServiceResponse<List<RepaymentDTO>>()
                                {
                                    Data = null,
                                    Success = false,
                                    Message = "Loan Applicaton Faled Valdaton",
                                    Errors = errors
                                };
                            }

                        }
                    }

                }
                else
                {
                    var member = await _context.Members
                         .Include(s => s.Person)
                        .FirstOrDefaultAsync(x => x.Id == loanData.MemberId);
                    LoanConfig config = await _context.LoanConfigs.Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                            .Include(s => s.Loan)
                            .Include(s => s.MemberType)
                            .FirstOrDefaultAsync();

                    if (config == null)
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                        error.ErrorMessage = "Loan not configured";
                        errors.Add(error);
                        return new ServiceResponse<List<RepaymentDTO>>()
                        {
                            Data = null,
                            Success = false,
                            Message = "Operation failed",
                            Errors = errors
                        };
                    }
                    else
                    {
                        if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                        {
                            decimal RepaymentAmount = Math.Round((loanData.LoanAmount) / loanData.RepaymntPeriod, 2);
                            decimal interest = (((decimal)loanData.InterestRate / 100) * loanData.LoanAmount) / 12;
                            decimal MonthlyRepayment = interest + RepaymentAmount;

                            int EffectiveMonth = loanData.EffectiveMonth;
                            int EffectiveYear = loanData.EffectiveYear;
                            if (loanData.EffectiveMonth == 12)
                            {
                                EffectiveYear = loanData.EffectiveYear + 1;
                                EffectiveMonth = 1;
                            }


                            DateTime LoanEffectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 15);
                            DateTime LastRepaymentDate = LoanEffectiveDate;


                            var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);


                            LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                            loan.Interest = interest;
                            loan.RepaymentPeriod = loanData.RepaymntPeriod;
                            loan.Principal = RepaymentAmount;
                            loan.EffectiveDate = LoanEffectiveDate;
                            loan.MethodOfCollectionId = 2;
                            loan.DateSubmitted = DateTime.UtcNow;
                            if (loanData.LoanGuarantors.Count > 0)
                            {
                                loan.LoanGuarantors = loanGuarantors;
                                loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                            }
                            await _context.LoanApplications.AddAsync(loan);
                            await _context.SaveChangesAsync();
                            for (int counter = 1; counter <= loanData.RepaymntPeriod; counter++)
                            {
                                var nextRepaymentDate = LastRepaymentDate.AddMonths(counter);
                                RepaymentDTO repayment = new RepaymentDTO();
                                repayment.Principal = RepaymentAmount;
                                repayment.Interest = interest;
                                repayment.RepaymentDate = nextRepaymentDate;
                                repayment.TotalPayment = MonthlyRepayment;
                                repayment.LoanApplicationId = loan.Id;

                                repayments.Add(repayment);

                            }
                            await _context.LoanRepayments.AddRangeAsync(_mapper.Map<List<LoanRepayment>>(repayments));
                            await _context.SaveChangesAsync();
                            if (loanData.LoanGuarantors != null && loanData.LoanGuarantors.Count > 0)
                            {
                                foreach (var guarantor in loanData.LoanGuarantors)
                                {
                                    StringBuilder builder = new StringBuilder();

                                    var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                    var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));

                                    // var callbackUrl = "http://localhost:8080/guarantor/?code=" + encodedParams;
                                    var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                    //Send email
                                    string message = $"";
                                    builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                    builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                    builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                    builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                    builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));

                                    builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));

                                    try
                                    {
                                        await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                    }
                                    catch (Exception)
                                    {
                                    }

                                }
                            }
                            return new ServiceResponse<List<RepaymentDTO>>
                            {
                                Data = repayments,
                                Success = true,
                                Message = "Operation successful",
                                Errors = null
                            };
                        }
                        else
                        {
                            decimal RepaymentAmount = Math.Round(loanData.LoanAmount, 2);
                            decimal interest = (((decimal)config.IntrestRate / 100) * loanData.LoanAmount) / 12;
                            decimal MonthlyRepayment = Math.Round(interest + RepaymentAmount, 2);

                            DateTime LastRepaymentDate = new DateTime(loanData.EffectiveYear, loanData.EffectiveMonth, 1);

                            RepaymentDTO repayment = new RepaymentDTO();
                            repayment.Principal = RepaymentAmount;
                            repayment.Interest = interest;
                            repayment.Id = 1;
                            repayment.RepaymentDate = LastRepaymentDate;
                            repayment.TotalPayment = MonthlyRepayment;
                            repayments.Add(repayment);

                            var loanGuarantors = _mapper.Map<List<LoanGuarantor>>(loanData.LoanGuarantors);
                            List<LoanGuarantor> LoanGuarantors = new List<LoanGuarantor>();
                            LoanApplication loan = _mapper.Map<LoanApplication>(loanData);
                            loan.Interest = interest;
                            loan.RepaymentPeriod = loanData.RepaymntPeriod;
                            loan.Principal = RepaymentAmount;
                            loan.EffectiveDate = LastRepaymentDate;
                            loan.MethodOfCollectionId = 2;
                            loan.DateSubmitted = DateTime.UtcNow;
                            if (loanData.LoanGuarantors.Count > 0)
                            {
                                loan.LoanGuarantors = loanGuarantors;
                                loan.RequiredGuarantorsCount = loanData.LoanGuarantors.Count;
                            }

                            await _context.LoanApplications.AddAsync(loan);
                            await _context.SaveChangesAsync();

                            if (loanData.LoanGuarantors.Count > 0)
                            {
                                foreach (var guarantor in loanData.LoanGuarantors)
                                {
                                    StringBuilder builder = new StringBuilder();
                                    var urlParams = loan.Id + ":" + guarantor.EmployeeNumber + ":" + member.Id;
                                    var encodedParams = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(urlParams));

                                    // var callbackUrl = "http://localhost:8080/guarantor/?code=" + encodedParams;
                                    var callbackUrl = $"{_configuration["AppSettings:SiteInfo:Url"]}/guarantor?code= + encodedParams";
                                    builder.AppendLine(string.Format("<p>Dear {0}</p>", guarantor.GuarantorName));
                                    builder.AppendLine(string.Format("<p>CEMCS member with the details below has requested that you be his/her guarantor</p>"));
                                    builder.AppendLine(string.Format("<p>Member's Employee Number: {0}</p>", member.EmployeeNumber));
                                    builder.AppendLine(string.Format("<p>Full Name: {0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                    builder.AppendLine(string.Format("<p>Loan Amount: {0}</p>", string.Format("{0:#,0.00}", loanData.LoanAmount)));
                                    builder.AppendLine(string.Format("Please approve by clicking  <a href='{0}'>here</a>.", callbackUrl));
                                    try
                                    {
                                        await _emailSender.SendEmailAsync(guarantor.GuarantorEmail, "You have been selected as a guarantor", builder.ToString());
                                    }
                                    catch (Exception)
                                    {
                                    }

                                }
                            }
                            return new ServiceResponse<List<RepaymentDTO>>
                            {
                                Data = null,
                                Success = true,
                                Message = "Operation successful",
                                Errors = null
                            };
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                List<Error> exceptions = new List<Error>();
                Error exception = new Error();
                exception.ErrorCode = Enums.ApplicatonResponseCodes.ExceptionOccured.ToString();
                exception.ErrorMessage = ex.Message;
                exceptions.Add(exception);
                return new ServiceResponse<List<RepaymentDTO>>
                {
                    Data = null,
                    Success = false,
                    Message = "Operation failed",
                    Errors = exceptions
                };
            }


        }
        public async Task<ValidationDTO> ValidateMimeLoanApplication(MimeLoanPostDTO loanData)
        {
            ValidationDTO validation = new ValidationDTO();
            List<Error> errors = new List<Error>();
            var ValidationFailed = false;
            try
            {
                var member = await _context.Members.Where(x => x.Id == loanData.MemberId).FirstOrDefaultAsync();
                if (member == null)
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.ItemNotFound.ToString();
                    error.ErrorMessage = "Invalid Member Supplied";
                    errors.Add(error);
                    ValidationFailed = true;
                }
                else
                {

                    var config = await _context.LoanConfigs
                        .Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                        .Include(s => s.Loan)
                        .Include(s => s.MemberType)
                        .FirstOrDefaultAsync();
                    if (config == null)
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                        error.ErrorMessage = "Loan not configured";
                        errors.Add(error);
                        ValidationFailed = true;
                    }
                    else
                    {

                        //var Savings = await _memberSavingService.GetSavingsByMemId(loanData.MemberId);
                        decimal balance = 0;
                        balance = await _memberBalanceService.GetMeberSavingsBalances((int)Enums.SavingsType.savings, Enums.BalanceType.MemberSavings, loanData.MemberId);
                        //if (Savings != null && Savings.Count > 0)
                        //{
                        //    var SavingsBalance = Savings.FirstOrDefault(x => x.Type == (int)Enums.SavingsType.savings);
                        //    balance = await _memberBalanceService.GetMeberSavingsBalances(SavingsBalance.Id, Enums.BalanceType.MemberSavings, loanData.MemberId);  
                        //}

                        if (member.MemberType.Equals(Enums.MemberType.Retiree))
                        {
                            if (loanData.FormFile.Length == 0)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Kindly upload your recent payslip",
                                    ErrorCode = Enums.ApplicatonResponseCodes.PayslipUploadError.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            else
                            {
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", loanData.FormFile.FileName);
                                var ext = Path.GetExtension(path).ToLowerInvariant();
                                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                                {
                                    // The extension is invalid ... discontinue processing the file
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Kindly uoload accepted format",
                                        ErrorCode = Enums.ApplicatonResponseCodes.PayslipUploadError.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }

                            }

                        }
                        if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                        {
                            decimal MinAmount = config.MinLoanAmount;
                            decimal MaxLoanAmount = config.MaxLoanAmount;
                            int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                            int MaximumRepaymentPeriod = config.MaxMonthlyRepayPeriod;
                            decimal lumpSumSavings = config.LumpSumSavingsAmount;

                            var concurrent = _context.LoanApplications.Where(x => x.MemberId == loanData.MemberId && x.LoanId == loanData.LoanId && (x.LoanCondition == Enums.LoanConditionStatus.Pending || x.LoanCondition == Enums.LoanConditionStatus.Running)).ToList();
                            if (concurrent != null && concurrent.Count > 0)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "You have a running or pending loan.",
                                    ErrorCode = Enums.ApplicatonResponseCodes.PendingorRunningLoan.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }

                            if (MinAmount != 0 && loanData.LoanAmount < MinAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Below Allowable Minimum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MaxLoanAmount != 0 && loanData.LoanAmount > MaxLoanAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Above Allowable Maximum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod < MinimumRepaymentReriod)
                            {
                                var error = new Error()
                                {
                                    //ErrorMessage = "Repayment Period Less than Minimum Repayment Period
                                    ErrorMessage = "Invalid Repayment Period Supplied (Below Minimum)",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod > MinimumRepaymentReriod)
                            {
                                var error = new Error()
                                {
                                    // ErrorMessage = "Repayment Period more than Minimum Repayment Period",
                                    ErrorMessage = "Invalid Repayment Period Supplied (Above Maximum)",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (lumpSumSavings > 0)
                            {
                                decimal savingParam = (lumpSumSavings / 100) * loanData.LoanAmount;
                                if (balance < savingParam)
                                {
                                    var error = new Error()
                                    {
                                        //ErrorMessage = "Not enough savings to apply for loan",
                                        ErrorMessage = "Your savings is not enough to apply for this loan",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                            }

                        }
                        if (config.Loan.LoanType == Enums.LoanType.TargetLoan)
                        {


                            decimal MaxLoanAmount = config.MaxLoanAmount;
                            int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                            decimal allowedAmount = (MaxLoanAmount / 100) * loanData.ExpectedAmount;

                            if (loanData.LoanAmount > allowedAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Above Maximum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }



                        }

                    }
                }

            }
            catch (Exception ex)
            {
                var error = new Error()
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = Enums.ApplicatonResponseCodes.ExceptionOccured.ToString()
                };
                ValidationFailed = true;
                errors.Add(error);
            }
            validation.Errors = errors;
            validation.Status = ValidationFailed;
            return validation;
        }
        public async Task<ValidationDTO> ValidateLoanApplication(LoanPostDTO loanData)
        {
            ValidationDTO validation = new ValidationDTO();
            List<Error> errors = new List<Error>();
            var ValidationFailed = false;
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == loanData.MemberId);
                if (member == null)
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.ItemNotFound.ToString();
                    error.ErrorMessage = "Invalid Member Supplied";
                    errors.Add(error);
                    ValidationFailed = true;
                }
                else
                {

                    var config = await _context.LoanConfigs
                        .Where(x => x.LoanId == loanData.LoanId && x.MemberTypeId == (int)member.MemberType)
                        .Include(s => s.Loan)
                        .Include(s => s.MemberType)
                        .FirstOrDefaultAsync();
                    if (config == null)
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidDataSupplied.ToString();
                        error.ErrorMessage = "Loan not configured";
                        errors.Add(error);
                        ValidationFailed = true;
                    }
                    else
                    {

                        //var Savings = await _memberSavingService.GetSavingsByMemId(loanData.MemberId);
                        decimal balance = 0;
                        balance = await _memberBalanceService.GetMeberSavingsBalances((int)Enums.SavingsType.savings, Enums.BalanceType.MemberSavings, loanData.MemberId);
                        //if (Savings != null && Savings.Count > 0)
                        //{
                        //    var SavingsBalance = Savings.FirstOrDefault(x => x.Type == (int)Enums.SavingsType.savings);
                        //    balance = await _memberBalanceService.GetMeberSavingsBalances(SavingsBalance.Id, Enums.BalanceType.MemberSavings, loanData.MemberId);  
                        //}
                        if (config.Loan.LoanType == Enums.LoanType.RegularLoan || config.Loan.LoanType == Enums.LoanType.ExecutiveLoan)
                        {
                            decimal MinAmount = config.MinLoanAmount;
                            decimal MaxLoanAmount = config.MaxLoanAmount;
                            int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                            int MaximumRepaymentPeriod = config.MaxMonthlyRepayPeriod;
                            decimal lumpSumSavings = config.LumpSumSavingsAmount;

                            var concurrent = _context.LoanApplications.Where(x => x.MemberId == loanData.MemberId && x.LoanId == loanData.LoanId && (x.LoanCondition == Enums.LoanConditionStatus.Pending || x.LoanCondition == Enums.LoanConditionStatus.Running)).ToList();
                            if (concurrent != null && concurrent.Count > 0)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "You have a running or pending loan.",
                                    ErrorCode = Enums.ApplicatonResponseCodes.PendingorRunningLoan.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }

                            if (MinAmount != 0 && loanData.LoanAmount < MinAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Below Minimum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MaxLoanAmount != 0 && loanData.LoanAmount > MaxLoanAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Above Maximum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod < MinimumRepaymentReriod)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Repayment Period Less than Minimum Repayment Period",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (MinimumRepaymentReriod != 0 && loanData.RepaymntPeriod > MinimumRepaymentReriod)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Repayment Period more than Minimum Repayment Period",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }
                            if (lumpSumSavings > 0)
                            {
                                decimal savingParam = (lumpSumSavings / 100) * loanData.LoanAmount;
                                if (balance < savingParam)
                                {
                                    var error = new Error()
                                    {
                                        ErrorMessage = "Not enough savings to apply for loan",
                                        ErrorCode = Enums.ApplicatonResponseCodes.InvalidRepaymentPeriodSupplied.ToString()
                                    };
                                    errors.Add(error);
                                    ValidationFailed = true;
                                }
                            }

                        }
                        if (config.Loan.LoanType == Enums.LoanType.TargetLoan)
                        {


                            decimal MaxLoanAmount = config.MaxLoanAmount;
                            int MinimumRepaymentReriod = config.MinMonthlyRepayPeriod;
                            decimal allowedAmount = (MaxLoanAmount / 100) * loanData.ExpectedAmount;

                            if (loanData.LoanAmount > allowedAmount)
                            {
                                var error = new Error()
                                {
                                    ErrorMessage = "Loan Amount Above Maximum Amount",
                                    ErrorCode = Enums.ApplicatonResponseCodes.InvalidAmountSupplied.ToString()
                                };
                                errors.Add(error);
                                ValidationFailed = true;
                            }



                        }

                    }
                }

            }
            catch (Exception ex)
            {
                var error = new Error()
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = Enums.ApplicatonResponseCodes.ExceptionOccured.ToString()
                };
                ValidationFailed = true;
                errors.Add(error);
            }
            validation.Errors = errors;
            validation.Status = ValidationFailed;
            return validation;
        }

        public async Task<ServiceResponse<List<LoanApplicationDTO>>> GetLoansByMemberId(int MemberId)
        {
            ServiceResponse<List<LoanApplicationDTO>> response = new ServiceResponse<List<LoanApplicationDTO>>();
            List<LoanApplication> applyLoanDTO = await _context.LoanApplications
            .Where(c => c.MemberId == MemberId)
            .Include(l => l.Loan)
            .Include(m => m.Member)
            .ThenInclude(p => p.Person)
            .ToListAsync();
            response.Data = _mapper.Map<List<LoanApplicationDTO>>(applyLoanDTO);
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<LoanApplicationDTO>>> GetLoansByEmployeeNumber(string EmployeeNumber)
        {

            ServiceResponse<List<LoanApplicationDTO>> response = new ServiceResponse<List<LoanApplicationDTO>>();
            List<LoanApplication> applyLoanDTO = await _context.LoanApplications
            .Where(c => c.Member.EmployeeNumber == EmployeeNumber)
            .Include(l => l.Loan)
            .Include(m => m.Member)
            .ThenInclude(p => p.Person)
            .ToListAsync();
            response.Data = _mapper.Map<List<LoanApplicationDTO>>(applyLoanDTO);
            response.Success = true;
            return response;
        }

        async Task<PendingApproval> SubmitForApproval(ModuleApprover moduleApprover, int itemId)
        {
            PendingApproval pendingApproval = new PendingApproval
            {
                ModuleApproverId = moduleApprover.Id,
                ItemId = itemId,
                Approved = null,
                CurrentLevel = 0
            };

            _context.PendingApprovals.Add(pendingApproval);
            await _context.SaveChangesAsync();
            return pendingApproval;
        }

        public async Task<ServiceResponse<List<LoanGuarantorDTO>>> GetGuarantors(int MemberId)
        {
            ServiceResponse<List<LoanGuarantorDTO>> response = new ServiceResponse<List<LoanGuarantorDTO>>();
            var gs = new List<LoanGuarantor>();
            var gg = await _context.LoanGuarantors.Where(x => x.LoanApplication.MemberId == MemberId).Distinct().ToListAsync();

            var distinctG = new List<LoanGuarantor>();
            foreach (var fg in gg)
            {
                var exists = distinctG.FirstOrDefault(x => x.EmployeeNumber == fg.EmployeeNumber);
                if (exists == null)
                {
                    distinctG.Add(fg);
                }
            }
            //var g = loanGuarntors.Select(x => x.LoanGuarantors).ToList();
            response.Data = _mapper.Map<List<LoanGuarantorDTO>>(distinctG);
            response.Success = true;
            return response;
        }
    }
}