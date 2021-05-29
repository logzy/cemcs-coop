namespace COOP.Banking.BusinessEntities
{
    public class WaiverApplicationDetail
    {
        public int Id { get; set; }
        public int WaiverApplicationId { get; set; }
        public int WaiverTypeId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Interest { get; set; }
        public decimal Fee { get; set; }

        public WaiverApplication WaiverApplication { get; set; }
        public WaiverType WaiverType { get; set; }

    }
}
