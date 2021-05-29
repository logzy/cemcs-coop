namespace COOP.Banking.Data
{
    public class Enums
    {
        public enum LoanType
        {
            TargetLoan = 1,
            ExecutiveLoan = 2,
            RegularLoan = 3
        }
        public enum MemberType
        {
            Regular = 1,
            Retiree = 2,
            Expatriate = 3
        }
        public enum AdminChargeType
        {
            OneMonthnterestOnCurrentLoan = 1,
            Flat = 2,
            CurrentLoanAmount

        }
        public enum SavingsType
        {
            savings = 1,
            deposit = 2
        }
        public enum UserType
        {
            Admin,
            User,
            Member,
            Employee
        }
        public enum ApproverType
        {
            Individual = 1,
            Group = 2,
        }
        public enum ApprovalStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }

        public enum FormAction
        {
            Validate = 1,
            Save = 2
        }
        public enum ApplicatonResponseCodes
        {
            InvalidDataSupplied,
            InvalidAmountSupplied,
            InvalidDateSupplied,
            ItemNotFound,
            InvalidRepaymentPeriodSupplied,
            LowSavingsAmount,
            ExceptionOccured,
            PendingorRunningLoan,
            InvalidBalance,
            ApproversNotSetup,
            PayslipUploadError
        }
        public enum LoanConditionStatus
        {
            Pending = 0,
            Running = 1,
            Settled = 2,
            Default = 3
        }
        public enum BalanceType
        {
            MemberSavings = 1,
            Loans = 2,
        }
        public enum PaymentStatus
        {
            Pending = 0,
            Paid = 1,
        }

    }
}
