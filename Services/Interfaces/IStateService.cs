using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IStateService
    {
        public Task<List<State>> GetStates();
        public Task<State> GetState(int stateId);
        public Task<State> SaveState(State state);
        public Task<State> UpdateState(State state);
    }
}
