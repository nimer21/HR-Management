using System.ComponentModel;

namespace HR_Management.Models
{
    public class UserActivity
    {
        [DisplayName("Created By")]
        public string? CreatedById { set; get; }
        [DisplayName("Created By")]
        public ApplicationUser CreatedBy { set; get; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { set; get; }
        [DisplayName("Modified By")]
        public string? ModifiedById { set; get; }
        [DisplayName("Modified By")]
        public ApplicationUser ModifiedBy { set; get; }
        [DisplayName("Modified On")]
        public DateTime ModifiedOn { set; get; }

    }
    public class ApprovalActivity : UserActivity
    {
        [DisplayName("Approved By")]
        public string? ApprovedById { set; get; }
        [DisplayName("Approved By")]
        public ApplicationUser ApprovedBy { set; get; }
        [DisplayName("Approved On")]
        public DateTime ApprovedOn { set; get; }
        //public string? RejectedById { set; get; }
        //public DateTime RejectedOn { set; get; }

    }
}
