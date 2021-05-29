namespace COOP.Banking.Data.DTO
{
    public class SavingsConfigurationDTO
    {
        public int? Id { get; set; }
        public int MemberTypeId { get; set; }
        public decimal MinimumSavingsAmount { get; set; }
    }
}