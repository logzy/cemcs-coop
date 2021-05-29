using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class MigrationService : IMigrationService
    {

        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemberService _memberService;
        private readonly IMemberSavingService _memberSavingService;
        private readonly IEmailSender _emailSender;
        private readonly IEmployeeService _employeeService;
        public MigrationService(CoopBankingDataContext context, IMapper mapper, IOptions<AppSettings> appSettings, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IMemberService memberService,
            IMemberSavingService memberSavingService,
            IEmailSender emailSender,
            IEmployeeService employeeService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _memberService = memberService;
            _memberSavingService = memberSavingService;
            _emailSender = emailSender;
            _employeeService = employeeService;
        }

        public async Task MigrateMemberBalances()
        {
            //var RetireeBalances = _context.CreditCommitteeBalances.ToList();
            //List<MemberBalance> mbs = new List<MemberBalance>();
            //foreach (var balance in RetireeBalances)
            //{

            //    var Member = await _context.Members.FirstOrDefaultAsync(x => x.EmployeeNumber.Equals(balance.EMPNO.Trim()));
            //    if (Member != null)
            //    {
            //        decimal sb, dp, st, lt, hp, vl, t1, t2, t3, ex;

            //        var SavingsBalance = Decimal.TryParse(balance.Savgs, out sb);
            //        var DepositBalance = Decimal.TryParse(balance.SDEP, out dp);
            //        var ShortBalance = Decimal.TryParse(balance.STLoan, out st);
            //        var LongBalance = Decimal.TryParse(balance.STLoan, out lt);

            //        var VehicleBalance = Decimal.TryParse(balance.Vehicle, out vl);
            //        var HomeBalance = Decimal.TryParse(balance.HAPL, out hp);

            //        var t1Balance = Decimal.TryParse(balance.Vehicle, out t1);
            //        var t2Balance = Decimal.TryParse(balance.HAPL, out t2);
            //        var t3Balance = Decimal.TryParse(balance.Vehicle, out t3);
            //        var ExBalance = Decimal.TryParse(balance.HAPL, out ex);


            //        MemberBalance savings = new MemberBalance();
            //        savings.BalanceType = Enums.BalanceType.MemberSavings;
            //        savings.ItemId = (int)Enums.SavingsType.deposit;
            //        savings.MemberId = Member.Id;
            //        savings.CurrentBalance = sb;
            //        mbs.Add(savings);

            //        MemberBalance deposit = new MemberBalance();
            //        deposit.BalanceType = Enums.BalanceType.MemberSavings;
            //        deposit.ItemId = (int)Enums.SavingsType.deposit;
            //        deposit.MemberId = Member.Id;
            //        deposit.CurrentBalance = dp;
            //        mbs.Add(deposit);

            //        MemberBalance longTerm = new MemberBalance();
            //        longTerm.BalanceType = Enums.BalanceType.Loans;
            //        longTerm.ItemId = 1;
            //        longTerm.MemberId = Member.Id;
            //        longTerm.CurrentBalance = lt;
            //        mbs.Add(longTerm);

            //        MemberBalance shortTerm = new MemberBalance();
            //        shortTerm.BalanceType = Enums.BalanceType.Loans;
            //        shortTerm.ItemId = 3;
            //        shortTerm.MemberId = Member.Id;
            //        shortTerm.CurrentBalance = st;
            //        mbs.Add(shortTerm);

            //        MemberBalance homeTerm = new MemberBalance();
            //        homeTerm.BalanceType = Enums.BalanceType.Loans;
            //        homeTerm.ItemId = 5;
            //        homeTerm.MemberId = Member.Id;
            //        homeTerm.CurrentBalance = hp;
            //        mbs.Add(homeTerm);

            //        MemberBalance vehicleTerm = new MemberBalance();
            //        vehicleTerm.BalanceType = Enums.BalanceType.Loans;
            //        vehicleTerm.ItemId = 4;
            //        vehicleTerm.MemberId = Member.Id;
            //        vehicleTerm.CurrentBalance = vl;
            //        mbs.Add(vehicleTerm);

            //        MemberBalance t1l = new MemberBalance();
            //        t1l.BalanceType = Enums.BalanceType.Loans;
            //        t1l.ItemId = 7;
            //        t1l.MemberId = Member.Id;
            //        t1l.CurrentBalance = t1;
            //        mbs.Add(t1l);

            //        MemberBalance t2l = new MemberBalance();
            //        t2l.BalanceType = Enums.BalanceType.Loans;
            //        t2l.ItemId = 8;
            //        t2l.MemberId = Member.Id;
            //        t2l.CurrentBalance = t2;
            //        mbs.Add(t2l);

            //        MemberBalance t3l = new MemberBalance();
            //        t3l.BalanceType = Enums.BalanceType.Loans;
            //        t3l.ItemId = 9;
            //        t3l.MemberId = Member.Id;
            //        t3l.CurrentBalance = t3;
            //        mbs.Add(t3l);

            //        MemberBalance exl = new MemberBalance();
            //        exl.BalanceType = Enums.BalanceType.Loans;
            //        exl.ItemId = 6;
            //        exl.MemberId = Member.Id;
            //        exl.CurrentBalance = ex;
            //        mbs.Add(t3l);
            //    }

            //}
            //await _context.MemberBalances.AddRangeAsync(mbs);
            //await _context.SaveChangesAsync();
        }

        public async Task MigrateMembers()
        {
      //      var users = _userManager.Users.ToList();
      //      string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
      //                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
      //                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
      //                   "M/d/yyyy h:mm", "M/d/yyyy h:mm",
      //                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
      //      foreach (var user in users)
      //      {
      //          var muser = user.UserName;
      //          var retiree = _context.Retireeexport.Where(x => x.EMPNO == muser).FirstOrDefault();
      //          if (retiree != null)
      //          {
      //              var MemberExist = _context.Members.Where(x => x.EmployeeNumber == retiree.EMPNO).FirstOrDefault();

      //              if (MemberExist == null)
      //              {
      //                  Person p = new Person();
      //                  p.Address1 = string.Empty;
      //                  p.Address2 = string.Empty;

      //                  p.Country = (retiree.NATIONALITY.ToLower().Contains("nigeria")) ? "Nigeria" : string.Empty;
      //                  p.CreatedBy = "system";
      //                  p.DateCreated = DateTime.UtcNow;
      //                  p.Email = retiree.EMAILADDRESS;
      //                  p.PersonalEmail = retiree.ALTEMAILADDRESS;
      //                  p.WorkPhone = retiree.ALTPHONE;
      //                  p.MobileNumber = retiree.DIRECTPHONE;
      //                  //DateTime RetirementDate;
      //                  DateTime RetirementDate = Convert.ToDateTime(retiree.RETIREMENTDATE,
      //System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);



      //                  Member member = new Member();
      //                  member.UserId = user.Id;
      //                  member.Active = false;
      //                  member.Deleted = false;
      //                  member.HasPaidFee = true;
      //                  member.Tag = Guid.NewGuid().ToString();
      //                  if (retiree.EMPNO.Contains("R"))
      //                  {
      //                      member.MemberType = Enums.MemberType.Retiree;
      //                  }
      //                  else if (retiree.EMPNO.Contains("E"))
      //                  {
      //                      member.MemberType = Enums.MemberType.Expatriate;
      //                  }
      //                  else
      //                  {
      //                      member.MemberType = Enums.MemberType.Regular;
      //                  }

      //                  member.Location = retiree.LOCATIONNAME;
      //                  member.EmployeeNumber = retiree.EMPNO;
      //                  member.RetirementDate = RetirementDate;
      //                  if (!string.IsNullOrEmpty(retiree.EMPLOYMENTDATE))
      //                  {
      //                      DateTime EmploymentDate = Convert.ToDateTime(retiree.EMPLOYMENTDATE,
      //System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
      //                      member.EmploymentDate = EmploymentDate;
      //                  }


      //                  p.DateCreated = DateTime.UtcNow;
      //                  p.CreatedBy = "system";
      //                  p.LastModifiedBy = "user";
      //                  p.StateId = 25;

      //                  member.Person = p;

      //                  await _memberService.SaveMember(member);

      //                  // Get newly created member and insert its savings into the member savings table
      //                  Member addedMember = await _memberService.GetMemberByUserId(member.UserId);
      //                  //_memberSavingService.CreateNewMemberSavingAndDeposit(0, addedMember.Id);
      //              }

      //          }
      //      }
      //      await _context.SaveChangesAsync();
           
        }

  //      public async Task MigrateEmployees()
  //      {
  //          var employees = _context.EmployeeDetails.ToList();
  //          foreach (var employee in employees)
  //          {
  //              var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == employee.EMPNO);
  //              if (user != null)
  //              {
  //                  Person p = new Person();
  //                  p.Address1 = string.Empty;
  //                  p.Address2 = string.Empty;

  //                  p.Country = "Nigeria";
  //                  p.CreatedBy = "system";
  //                  p.DateCreated = DateTime.UtcNow;
  //                  p.Email = employee.EMAIL_ADDR;
  //                  p.PersonalEmail = String.Empty;
  //                  p.WorkPhone = employee.WORK_PHONE;
  //                  p.MobileNumber = employee.MOBILE_PHONE;
                   
  //                  if (!string.IsNullOrEmpty(employee.DATE_EMPLOY))
  //                  {
  //                      DateTime BirthDate = Convert.ToDateTime(employee.DATE_EMPLOY, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
  //                      p.DateOfBirth = BirthDate;
  //                  }
  //                  p.FirstName = employee.FIRSTNAME;
  //                  p.LastName = employee.SURNAME;
  //                  p.Sex = employee.GENDER;
  //                  p.StateId = Convert.ToInt32(employee.STATEORIGING);
  //                  p.MiddleName = employee.OTHERNAME;
  //                  p.MaritalStatus = employee.MARITAL_STATUS;
                   

  //                  Employee emp = new Employee();
  //                  emp.UserId = user.Id;
  //                  emp.Active = false;
  //                  emp.Deleted = false;
  //                  emp.Deleted = false;
  //                  emp.DepartmentId = Convert.ToInt32(employee.EMPLOYEE_DEPT);
  //                  emp.EmployeeNumber = user.UserName;
  //                  emp.EmployeeTypeId = Convert.ToInt32(employee.EMPLOYEE_TYPE);
  //                  emp.JobTitle = employee.JOB_TITLE;
  //                  emp.StateOfOriginId = Convert.ToInt32(employee.STATEORIGING);
  //                  emp.BasicSalary = decimal.Parse(employee.MONTH_BASIC_SAL);
  //                  emp.AnnualSalary = decimal.Parse(employee.ANNUAL_BASIC_SAL);
  //                  if (!string.IsNullOrEmpty(employee.DATE_EMPLOY))
  //                  {
  //                      DateTime EmploymentDate = Convert.ToDateTime(employee.DATE_EMPLOY,
  //System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
  //                      emp.DateOfHire = EmploymentDate;
  //                  }
      

  //                  p.DateCreated = DateTime.UtcNow;
  //                  p.CreatedBy = "system";
  //                  p.LastModifiedBy = "system";
  //                  p.StateId = 25;

  //                  emp.Person = p;

  //                  await _employeeService.SaveEmployee(emp);
  //              }
  //          }
  //      }
        public async Task MigrateRetireeBalances()
        {
            //var RetireeBalances = _context.RetireeBalances.ToList();
            //List<MemberBalance> mbs = new List<MemberBalance>();
            //foreach (var balance in RetireeBalances)
            //{

            //    var Member = await _context.Members.FirstOrDefaultAsync(x => x.EmployeeNumber.Equals(balance.EMPNO.Trim()));
            //    if (Member != null)
            //    {
            //        decimal sb, dp, st, lt;

            //        var SavingsBalance = Decimal.TryParse(balance.SAVINGS, out sb);
            //        var DepositBalance = Decimal.TryParse(balance.SPECDEP, out dp);
            //        var ShortBalance = Decimal.TryParse(balance.SHORTTERM, out st);
            //        var LongBalance = Decimal.TryParse(balance.LONGTERM, out lt);

            //        MemberBalance savings = new MemberBalance();
            //        savings.BalanceType = Enums.BalanceType.MemberSavings;
            //        savings.ItemId = (int)Enums.SavingsType.deposit;
            //        savings.MemberId = Member.Id;
            //        savings.CurrentBalance = sb;
            //        mbs.Add(savings);

            //        MemberBalance deposit = new MemberBalance();
            //        deposit.BalanceType = Enums.BalanceType.MemberSavings;
            //        deposit.ItemId = (int)Enums.SavingsType.deposit;
            //        deposit.MemberId = Member.Id;
            //        deposit.CurrentBalance = dp;
            //        mbs.Add(deposit);

            //        MemberBalance longTerm = new MemberBalance();
            //        longTerm.BalanceType = Enums.BalanceType.Loans;
            //        longTerm.ItemId = 1;
            //        longTerm.MemberId = Member.Id;
            //        longTerm.CurrentBalance = lt;
            //        mbs.Add(longTerm);

            //        MemberBalance shortTerm = new MemberBalance();
            //        shortTerm.BalanceType = Enums.BalanceType.Loans;
            //        shortTerm.ItemId = 3;
            //        shortTerm.MemberId = Member.Id;
            //        shortTerm.CurrentBalance = st;
            //        mbs.Add(shortTerm);
            //    }

            //}
            //await _context.MemberBalances.AddRangeAsync(mbs);
            //await _context.SaveChangesAsync();
        }


        public async Task MigrateRetireeMembers()
        {

            //var retirees = _context.Retireeexport.ToList();
            //foreach (var retiree in retirees)
            //{
            //    var userExists = await _userManager.FindByNameAsync(retiree.EMPNO);
            //    if (userExists == null)
            //    {
            //        ApplicationUser user = new ApplicationUser()
            //        {
            //            Email = retiree.EMAILADDRESS,
            //            SecurityStamp = Guid.NewGuid().ToString(),
            //            UserName = retiree.EMPNO,
            //            UserType = Enums.UserType.Member,
            //            UserTypeCategory = (int)Enums.MemberType.Regular
            //        };
            //        try
            //        {
            //            var result = await _userManager.CreateAsync(user, retiree.PINNO);
            //            if (result.Succeeded)
            //            {

            //                try
            //                {

            //                }
            //                catch (Exception)
            //                {
            //                }
            //            }
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    }



            //}

           
        }

        public Task UpdateMemberData()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRetireeData()
        {
            //var retirees = _context.Retireeexport.ToList();
            //foreach (var retiree in retirees)
            //{
            //    var userExists = await _context.Members.Include(p => p.Person).FirstOrDefaultAsync(x => x.EmployeeNumber == retiree.EMPNO);
            //    if (userExists != null)
            //    {
            //        userExists.Person.FirstName = retiree.FIRSTNAME;
            //        userExists.Person.LastName = retiree.SURNAME;
            //        userExists.Person.MiddleName = retiree.MIDDLENAME;

            //        _context.Members.Update(userExists);
            //        await _context.SaveChangesAsync();
            //    }



            //}

        }

        //public async Task MigrateEmployeesUsers()
        //{
        //    var retirees = _context.EmployeeDetails.ToList();
        //    foreach (var retiree in retirees)
        //    {
        //        var userExists = await _userManager.FindByNameAsync(retiree.EMPNO);
        //        if (userExists == null)
        //        {
        //            ApplicationUser user = new ApplicationUser()
        //            {
        //                Email = retiree.EMPLOYEE_CODE,
        //                SecurityStamp = Guid.NewGuid().ToString(),
        //                UserName = retiree.EMPNO,
        //                UserType = Enums.UserType.Employee,
        //                UserTypeCategory = 0
        //            };
        //            try
        //            {
        //                var result = await _userManager.CreateAsync(user, retiree.PINNO);
        //                if (result.Succeeded)
        //                {

        //                    try
        //                    {

        //                    }
        //                    catch (Exception)
        //                    {
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //            }
        //        }



        //    }

        //}
    }
}
