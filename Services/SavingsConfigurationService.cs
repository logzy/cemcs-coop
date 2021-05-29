using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class SavingsConfigurationService : ISavingsConfigurationService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;

        public SavingsConfigurationService(CoopBankingDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<SavingsConfiguration>> CreateSavings(SavingsConfigurationDTO newSavings)
        {
            ServiceResponse<SavingsConfiguration> response = new ServiceResponse<SavingsConfiguration>();

            SavingsConfiguration savings = _mapper.Map<SavingsConfiguration>(newSavings);

            if (await _context.SavingsConfigurations
                    .AnyAsync(c => c.MemberTypeId == newSavings.MemberTypeId))
            {
                response.Success = false;
                response.Message = "Minimum Savings Already Exist";
                return response;
            }

            await _context.SavingsConfigurations.AddAsync(savings);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<SavingsConfiguration>(savings);
            return response;
        }

        public async Task<SavingsConfigurationDTO> GetMinSavingById(int savingsId)
        {

            SavingsConfiguration savings = await _context.SavingsConfigurations
                                    .FirstOrDefaultAsync(c => c.MemberTypeId == savingsId);
            return _mapper.Map<SavingsConfigurationDTO>(savings);


        }

        public async Task<ServiceResponse<List<SavingsConfigurationDTO>>> GetMinSavings()
        {
            ServiceResponse<List<SavingsConfigurationDTO>> response = new ServiceResponse<List<SavingsConfigurationDTO>>();
            List<SavingsConfiguration> mSaving = await _context.SavingsConfigurations.ToListAsync();
            response.Data = (mSaving.Select(c => _mapper.Map<SavingsConfigurationDTO>(c))).ToList();
            return response;
        }
    }
}