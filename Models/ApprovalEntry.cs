﻿using System.ComponentModel;

namespace HR_Management.Models
{
    public class ApprovalEntry
    {
        public int Id { get; set; }
        [DisplayName("Record Id")]
        public int RecordId { get; set; } // 1
        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; } // Leave Application
        public SystemCodeDetail DocumentType { get; set; }
        [DisplayName("Sequence No")]
        public int SequenceNo { get; set; } // 1,2,3,4,5,6 (Approvals)
        [DisplayName("Approver Name")]
        public string ApproverId { get; set; } // 1,2,3,4,5,6 (Approvers)
        public ApplicationUser Approver { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; } // Status of the document
        public SystemCodeDetail Status { get; set; }
        [DisplayName("Date Sent for Approval")]
        public DateTime DateSentForApproval { get; set; } // Date sent for Approval
        [DisplayName("Last Modified On")]
        public DateTime LastModifiedOn { get; set; } // The Action of the Approvers
        [DisplayName("Last Modified By")]
        public string LastModifiedById { get; set; }
        public ApplicationUser LastModifiedBy { get; set; }
        [DisplayName("Comments")]
        public string Comments { get; set; }
        [DisplayName("Controller Name")]
        public string ControllerName { get; set; }
    }
}
