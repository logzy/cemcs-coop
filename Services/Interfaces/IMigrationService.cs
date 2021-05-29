using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IMigrationService
    {
        Task MigrateRetireeMembers();
        Task MigrateMembers();
        Task MigrateRetireeBalances();

        Task MigrateMemberBalances();

        Task UpdateRetireeData();
        Task UpdateMemberData();
        //Task MigrateEmployees();

        //Task MigrateEmployeesUsers();
    }
}
