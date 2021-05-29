using System;

namespace COOP.Banking.BusinessEntities
{
    public class Executive
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PositionId { get; set; }

        public Member Member { get; set; }
        public Position Position { get; set; }
    }
}
