﻿using System.ComponentModel.DataAnnotations;

namespace HR_Management.Models
{
    public class LeaveApplication : ApprovalActivity
    {
        public int Id { get; set; }
        [Display(Name ="Emplyee Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name ="No of Leave Days")]
        public int NoOfDays { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Leave Duration")]
        public int DurationId { get; set; }
        public SystemCodeDetail Duration { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string? Attachment { get; set; }
        [Display(Name = "Notes")]
        public string Description { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }
    }
}
