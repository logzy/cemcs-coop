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
    class StateService : IStateService
    {
        private readonly CoopBankingDataContext _context;
        public StateService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<State> GetState(int stateId)
        {
            var state = await _context.States
                .Where(p => p.Id == stateId)
                .FirstOrDefaultAsync();
            return state;
        }

        public async Task<List<State>> GetStates()
        {
            var states = await _context.States
                .ToListAsync();
            return states;
        }

        public async Task<State> SaveState(State state)
        {
            _context.States.Add(state);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return state;
        }
        public async Task<State> UpdateState(State state)
        {
            _context.Attach(state).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return state;
        }
    }
}
