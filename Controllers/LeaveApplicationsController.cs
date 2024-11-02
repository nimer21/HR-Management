using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Management.Data;
using HR_Management.Models;
using HR_Management.Data.Migrations;
using System.Security.Claims;

namespace HR_Management.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var awaitingStatus = _context.SystemCodeDetails
           .Include(x => x.SystemCode)
               .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "AwaitingApproval").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == awaitingStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ApprovedLeaveApplications()
        {
            var approvedStatus = _context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> RejectedLeaveApplications()
        {
            var rejectedStatus = _context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == rejectedStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
                .Include(x=>x.SystemCode)
                .Where(y=>y.SystemCode.Code== "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RejectLeave(int? id)
        {
            var leaveApplcation = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType).Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplcation == null) { return NotFound(); }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplcation);
        }
        [HttpPost]
        public async Task<IActionResult> RejectLeave(LeaveApplication leave)
        {
            var rejectStatus = _context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").FirstOrDefault();
            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType).Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null) { return NotFound(); }

            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "Nimer";
            leaveApplication.StatusId = rejectStatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync();

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            var leaveApplcation = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType).Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplcation == null) { return NotFound(); }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(leaveApplcation);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(LeaveApplication leave)
        {
            var approvedStatus = _context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();

            var adjustmenttype = _context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveAdjustment" && y.Code == "Negative").FirstOrDefault();
            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType).Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null) { return NotFound(); }

            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = Userid;
            leaveApplication.StatusId = approvedStatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(Userid);

            var adjustment = new LeaveAdjustmentEntry
            {
                EmployeeId = leaveApplication.EmployeeId,
                NoOfDays = leaveApplication.NoOfDays,
                LeaveStartDate = leaveApplication.StartDate,
                LeaveEndDate = leaveApplication.EndDate,
                AdjustmentDescription = "Leave Taken - Negative Adjustment",
                LeavePeriodId = 1,
                LeaveAdjustmentDate = DateTime.Now,
                AdjustmentTypeId = adjustmenttype.Id
            };
            _context.Add(adjustment);
            await _context.SaveChangesAsync(Userid);

            var employee = await _context.Employees.FindAsync(leaveApplication.EmployeeId);
            employee.LeaveOutStandingBalance = (employee.AllocatedLeaveDays - leaveApplication.NoOfDays);
            _context.Update(employee);
            await _context.SaveChangesAsync(Userid);

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
            .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }


        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication, IFormFile leaveattachment)
        {
            if (leaveattachment != null && leaveattachment.Length > 0)
            {
                var fileName = "LeaveAttachment_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + leaveattachment.FileName;
                var path = _configuration["FileSettings:UploadFolder"]!;
                var filepath = Path.Combine(path, fileName);
                var stream = new FileStream(filepath, FileMode.Create);
                await leaveattachment.CopyToAsync(stream);
                //stream.Close();
                leaveApplication.Attachment = fileName;

            }

            var pendingStatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();

            //if (ModelState.IsValid)
            //{
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            leaveApplication.CreatedOn = DateTime.Now;
                leaveApplication.CreatedById = Userid;
                leaveApplication.StatusId = pendingStatus.Id;
                _context.Add(leaveApplication);
                await _context.SaveChangesAsync(Userid);
                //return RedirectToAction(nameof(Index));
            //}

            //Leave Type
            var documenttype = await _context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code=="DocumentTypes" && x.Code == "LeaveApplication").FirstOrDefaultAsync();

            // Workflow UserGroup/  x.UserId == Userid &&
            var usergroup = await _context.ApprovalsUserMatrixs.Where(x =>  x.DocumentTypeId == documenttype.Id && x.Active==true).FirstOrDefaultAsync();

            var awaitingapproval = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();

            //Approvers/  && x.SenderId == Userid
            var approvers = await _context.WorkFlowUserGroupMembers.Where(x => x.WorkFlowUserGroupId == usergroup.WorkFlowUserGroupId).ToListAsync();

            foreach(var approver in approvers)
            {
                //Generate an Approval Entry
                var approvalentries = new ApprovalEntry()
                {
                    ApproverId = approver.ApproverId,
                    DateSentForApproval = DateTime.Now,
                    LastModifiedOn = DateTime.Now,
                    LastModifiedById = approver.SenderId,
                    RecordId = leaveApplication.Id,
                    ControllerName = "LeaveApplications",
                    DocumentTypeId = documenttype.Id,
                    SequenceNo = approver.SequenceNo,
                    StatusId = awaitingapproval.Id,
                    Comments = "Sent For Approval"                 
                };
                _context.Add(approvalentries);
                await _context.SaveChangesAsync(Userid);
            }   

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
                .Include(x=>x.SystemCode)
                .Where(y=>y.SystemCode.Code=="LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pendingStatus = await _context.SystemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();
                try
                {
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.ModifiedById = "Nimer";
                    leaveApplication.StatusId = pendingStatus.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}
