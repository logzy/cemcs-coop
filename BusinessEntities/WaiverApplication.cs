using System;
using System.Collections.Generic;

namespace COOP.Banking.BusinessEntities
{
    public class WaiverApplication
    {
        public WaiverApplication()
        {
            WaiverApplicationDetails = new HashSet<WaiverApplicationDetail>();
        }
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime ApplicatiinDate { get; set; }
        public decimal TotalWaiverFee { get; set; }
        public string Commnents { get; set; }
        public string ReceiptNo { get; set; }
        public int ApprovalCount { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? LastApprovalDate { get; set; }

        public Member Member { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        public ICollection<WaiverApplicationDetail> WaiverApplicationDetails { get; set; }

    }
}
