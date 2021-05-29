using AutoMapper;
using AutoMapper.Configuration;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class ModuleApproverNameStoreService: IModuleApproverNameStoreService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;
        private readonly IMemberSavingService _memberSavingService;
        private readonly IMemberBalanceService _memberBalanceService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IBankService _bankService;
        private readonly IEmailSender _emailSender;
        //private readonly IPendingApprovalService<ModuleApprover> _pendingApprovalService;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IConfiguration _configuration;

        public ModuleApproverNameStoreService(CoopBankingDataContext context, IMapper mapper, IMemberSavingService memberSavingService, IMemberBalanceService memberBalanceService, IEmailSender emailSender,
           IModuleApproverService moduleApproverService,
           IBeneficiaryService beneficiaryService,
           IBankService bankService,
           IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _memberSavingService = memberSavingService;
            _memberBalanceService = memberBalanceService;
            _emailSender = emailSender;
            _moduleApproverService = moduleApproverService;
            _beneficiaryService = beneficiaryService;
            _bankService = bankService;
            _configuration = configuration;
        }
        public async Task<List<ApproverDetailDTO>> GetApproverDetailsByLevel(int Module, int Level)
        {
            List<ApproverDetailDTO> ApproverDetails = new List<ApproverDetailDTO>();
            var Approvers = _context.ModuleApproverNameStores.FirstOrDefault(x => x.ModuleApproverId == Module && x.ApprovalLevel == Level);
            if (Approvers != null)
            {
                var ApproverNumbers = Approvers.Usernames;
                var ApproverArray = ApproverNumbers.Split(",");

                var Employees = _context.Employees.Where(x => ApproverArray.Contains(x.EmployeeNumber)).ToList();
                if(Employees!=null && Employees.Count > 0) { 
                }
                
            }
            return ApproverDetails;


        }
    }
}
