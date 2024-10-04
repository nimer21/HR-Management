﻿namespace HR_Management.Models
{
    public class UserActivity
    {
        public string? CreatedById { set; get; }
        public DateTime CreatedOn { set; get; }
        public string? ModifiedById { set; get; }
        public DateTime ModifiedOn { set; get; }

    }
    public class ApprovalActivity : UserActivity
    {
        public string? ApprovedById { set; get; }
        public DateTime ApprovedOn { set; get; }
        //public string? RejectedById { set; get; }
        //public DateTime RejectedOn { set; get; }

    }
}
