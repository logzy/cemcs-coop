using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MembersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberService _memberService;
        private readonly IMemberSavingService _memberSavingService;
        private readonly IEmployeeService _employeeService;
        private readonly IPendingApprovalService<ModuleApprover> _pendingApprovalService;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IMapper _mapper;
        public MembersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMemberService memberService,
            IEmployeeService employeeService,
            IMemberSavingService memberSavingService,
            IModuleApproverService moduleApproverService,
            IPendingApprovalService<ModuleApprover> pendingApprovalService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _memberService = memberService;
            _memberSavingService = memberSavingService;
            _employeeService = employeeService;
            _moduleApproverService = moduleApproverService;
            _pendingApprovalService = pendingApprovalService;
            _mapper = mapper;
        }

        // GET: /Members/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var members = await _memberService.GetMembers();
            var membersDTO = _mapper.Map<List<MemberDTO>>(members);

            //Response//
            ServiceResponse<List<MemberDTO>> response = new ServiceResponse<List<MemberDTO>>();
            response.Data = membersDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: /Members/Inactive
        [HttpGet("Inactive")]
        public async Task<ActionResult> GetInactiveAsync()
        {
            var members = await _memberService.GetInactiveMembers();
            var membersDTO = _mapper.Map<List<MemberDTO>>(members);

            //Response//
            ServiceResponse<List<MemberDTO>> response = new ServiceResponse<List<MemberDTO>>();
            response.Data = membersDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: /Members/Approved/1
        [HttpGet("{approve}")]
        public async Task<ActionResult> GetApprovedAsync(int approve)
        {
            var members = await _memberService.GetApprovedMembers(approve);
            var membersDTO = _mapper.Map<List<MemberDTO>>(members);

            //Response//
            ServiceResponse<List<MemberDTO>> response = new ServiceResponse<List<MemberDTO>>();
            response.Data = membersDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: Members/Details/
        [HttpGet("{memberId}")]
        public async Task<ActionResult> GetAsync(int memberId)
        {
            var member = await _memberService.GetMember(memberId);
            var memberDTO = _mapper.Map<MemberDTO>(member);

            //Response//
            ServiceResponse<MemberDTO> response = new ServiceResponse<MemberDTO>();
            response.Data = memberDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: Members/Usertype
        [HttpGet("Usertype")]
        public async Task<ActionResult> GetAsyncUserId()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.UserTypeCategory == 0)
            {
                ServiceResponse<EmployeeGetDTO> response = new ServiceResponse<EmployeeGetDTO>();

                var employee = await _employeeService.GetEmployeeByUserId(currentUser.Id);
                var employeeDTO = _mapper.Map<EmployeeGetDTO>(employee);
                employeeDTO.userLogin = currentUser.LastLogin;
                response.Data = employeeDTO;
                response.Success = true;
                return Ok(response);

            }
            if (currentUser.UserTypeCategory > 0)
            {
                ServiceResponse<MemberDTO> response = new ServiceResponse<MemberDTO>();

                var member = await _memberService.GetMemberByUserId(currentUser.Id);
                var memberDTO = _mapper.Map<MemberDTO>(member);
                memberDTO.userLogin = currentUser.LastLogin;
                memberDTO.IsCreditCommittee = await _memberService.IsCreditCommittee(member.Id);
                response.Data = memberDTO;
                response.Success = true;
                return Ok(response);
            }
            return Ok();



        }


        [HttpPost("EmpNumber")]
        public async Task<ActionResult> GetAsyncEmpNumber([FromBody] GetMemEmpNumberDTO empNum)
        {
            var member = await _memberService.GetMemberNumber(empNum.EmployeeNumber);
            var MemEmpNumDTO = _mapper.Map<GetMemEmpNumberDTO>(member);

            //Response//
            ServiceResponse<GetMemEmpNumberDTO> response = new ServiceResponse<GetMemEmpNumberDTO>();
            response.Data = MemEmpNumDTO;
            response.Success = true;
            return Ok(response);
        }


        [HttpGet]
        [Route("loginId")]
        public async Task<ActionResult> GetUserId()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            // return Ok(currentUser);
            if (currentUser.EmailConfirmed == true)
                return Ok(new
                {
                    Id = currentUser.Id,
                    UserType = currentUser.UserType,
                    UserName = currentUser.UserName,
                    UserTypeCategory = currentUser.UserTypeCategory,
                    Email = currentUser.Email
                });

            return BadRequest();
        }

        // POST: /Members
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] MemberDTO memberDTO)
        {
            ServiceResponse<MemberDTO> response = new ServiceResponse<MemberDTO>();
            //var currentUser = await _userManager.GetUserAsync(User);
            var existingMember = await _memberService.GetMember((int)memberDTO.Id);
            //if (existingMember.UserId == null)
            //  return NotFound("Member UserId is required");
            if (existingMember == null)
                return NotFound("Member not found");

            //if (existingMember.Approved != memberDTO.Approved)
            //{
            //    existingMember.Approved = memberDTO.Approved;
            //    await _memberService.UpdateMember(existingMember);
            //    response.Data = memberDTO;
            //    response.Success = true;
            //    response.Message = "Member Approved";
            //    return Ok(response);
            //}
            existingMember.EmployeeNumber = memberDTO.EmployeeNumber;
            existingMember.MemberType = memberDTO.MemberType;
            existingMember.Location = memberDTO.Location;
            existingMember.EmploymentDate = memberDTO.EmploymentDate;
            // existingMember.Active = memberDTO.Active;
            await _memberService.UpdateMember(existingMember);

            response.Data = memberDTO;
            response.Success = true;
            response.Message = "Member saved";
            return Ok(response);
        }

        // POST: /Members/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] MemberDTO memberDTO)
        {
            // Check if Member has module approver
            // Get module approver for member
            Module module = await _moduleApproverService.GetModuleByNormalizedName("MEMBERSHIPS");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
                return BadRequest("no module approver registered for membership registration");

            var currentUser = await _userManager.GetUserAsync(User);
            //Check if user has already registered as a member
            var userRegistered = await _memberService.GetMemberByUserId(currentUser.Id);
            if (userRegistered != null)
                return BadRequest("User already registered");

            if (string.IsNullOrEmpty(memberDTO.EmployeeNumber))
                return BadRequest("Employee number is required");
            var member = _mapper.Map<Member>(memberDTO);
            member.UserId = currentUser.Id;
            member.Active = false;
            member.Deleted = false;
            member.HasPaidFee = false;
            member.Tag = Guid.NewGuid().ToString();

            member.Person.DateCreated = DateTime.UtcNow;
            member.Person.CreatedBy = currentUser.UserName;
            member.Person.LastModifiedBy = "user";
            await _memberService.SaveMember(member);

            // Get newly created member and insert its savings into the member savings table
            Member addedMember = await _memberService.GetMemberByUserId(member.UserId);
            _memberSavingService.CreateNewMemberSavingAndDeposit(memberDTO.SavingsAmount, addedMember.Id);

            //Response//
            ServiceResponse<MemberDTO> response = new ServiceResponse<MemberDTO>();
            response.Data = memberDTO;
            response.Success = true;
            response.Message = "Member Registration successful, Savings and Deposit accounts added";
            return Ok(response);
        }

        // POST: /Members/CreatePending
        [HttpPost("CreatePending")]
        public async Task<ActionResult> CreatePendingAsync()
        {
            ServiceResponse<Member> response = new ServiceResponse<Member>();
            var currentUser = await _userManager.GetUserAsync(User);

            var existingMember = await _memberService.GetMemberByUserId(currentUser.Id);
            //if (existingMember.UserId == null)
            //  return NotFound("Member UserId is required");
            if (existingMember == null)
                return NotFound("Member not found");

            existingMember.HasPaidFee = true;
            // existingMember.Active = memberDTO.Active;
            await _memberService.UpdateMember(existingMember);

            // Get module approver for member
            Module module = await _moduleApproverService.GetModuleByNormalizedName("MEMBERSHIPS");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
            {
                response.Success = false;
                response.Message = "Module Approver for Memberships does not Exist";
                return Ok(response);
            }
            // Enter into the pending table
            await _pendingApprovalService.CreateApproval(moduleApprover, existingMember.Id);

            response.Data = existingMember;
            response.Success = true;
            response.Message = "Member Approval Created";
            return Ok(response);
        }
    }
}
