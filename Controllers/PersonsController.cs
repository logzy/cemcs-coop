using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class PersonsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public PersonsController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPersonService personService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _personService = personService;
            _mapper = mapper;
        }

        // GET: /Persons/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var persons = await _personService.GetPersons();
            var personsDTO = _mapper.Map<List<PersonDTO>>(persons);
            return Ok(personsDTO);
        }

        // GET: Persons/Details/5
        [HttpGet("{personId}")]
        public async Task<ActionResult> GetAsync(int personId)
        {
            var person = await _personService.GetPerson(personId);
            var personDTO = _mapper.Map<PersonDTO>(person);
            return Ok(personDTO);
        }

        // POST: /Persons
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            if (personDTO.Id == null)
            {
                var person = _mapper.Map<Person>(personDTO);
                person.DateCreated = DateTime.UtcNow;
                person.CreatedBy = "admin"; // currentUser.UserName;
                await _personService.SavePerson(person);
            }
            else
            {
                var existingPerson = await _personService.GetPerson((int)personDTO.Id);
                if (existingPerson == null)
                    return NotFound("Person not found");

                existingPerson.FirstName = personDTO.FirstName;
                existingPerson.LastName = personDTO.LastName;
                existingPerson.MiddleName = personDTO.MiddleName;
                existingPerson.Sex = personDTO.Sex;
                existingPerson.DateOfBirth = personDTO.DateOfBirth;
                existingPerson.Title = personDTO.Title;
                existingPerson.WorkPhone = personDTO.WorkPhone;
                existingPerson.MobileNumber = personDTO.MobileNumber;
                existingPerson.Email = personDTO.Email;
                existingPerson.Address1 = personDTO.Address1;
                existingPerson.Address2 = personDTO.Address2;
                existingPerson.StateId = personDTO.StateId;
                existingPerson.Country = personDTO.Country;
                existingPerson.MaritalStatus = personDTO.MaritalStatus;
                existingPerson.LastModifiedBy = "Admin"; //currentUser.UserName;
                existingPerson.LastModifiedDate = DateTime.UtcNow;
                existingPerson.PersonalEmail = personDTO.PersonalEmail;
                await _personService.UpdatePerson(existingPerson);
            }

            return Ok(personDTO);
        }

    }
}
