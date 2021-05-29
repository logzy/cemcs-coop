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
    class PersonService : IPersonService
    {
        private readonly CoopBankingDataContext _context;
        public PersonService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<Person> GetPerson(int personId)
        {
            var person = await _context.Persons
                .Where(p => p.Id == personId)
                .FirstOrDefaultAsync();
            return person;
        }

        public async Task<List<Person>> GetPersons()
        {
            var persons = await _context.Persons
                .ToListAsync();
            return persons;
        }

        public async Task<Person> SavePerson(Person person)
        {
            _context.Persons.Add(person);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }
        public async Task<Person> UpdatePerson(Person person)
        {
            _context.Attach(person).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }
    }
}
