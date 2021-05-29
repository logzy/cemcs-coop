using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IPersonService
    {
        public Task<List<Person>> GetPersons();
        public Task<Person> GetPerson(int personId);
        public Task<Person> SavePerson(Person person);
        public Task<Person> UpdatePerson(Person person);
    }
}
