﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Management.Data;
using HR_Management.Models;
using System.Security.Claims;
using HR_Management.ViewModels;
using AutoMapper;

namespace HR_Management.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public EmployeesController(IMapper mapper, IConfiguration configuration, ApplicationDbContext context)
        {
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(EmployeeViewModel employees)
        {
            //return View(await _context.Employees.Include(x => x.Status).ToListAsync());
            //EmployeeViewModel employees = new();
            var rawdata = _context.Employees.Include(x => x.Status).AsQueryable();
            if (!string.IsNullOrEmpty(employees.FullName.Trim()))
            {
                rawdata = rawdata
                    .Where(x => x.FullName.Contains(employees.FullName));
            }
            if (employees.PhoneNumber > 0)
            {
                rawdata = rawdata
                    .Where(x => x.PhoneNumber == employees.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(employees.EmailAddress))
            {
                rawdata = rawdata
                    .Where(x => x.EmailAddress.Contains(employees.EmailAddress));
            }
            if (!string.IsNullOrEmpty(employees.EmpNo))
            {
                rawdata = rawdata
                    .Where(x => x.EmpNo == employees.EmpNo);
            }
            employees.Employees = await rawdata.ToListAsync();

            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description");
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description");
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel newemployee, IFormFile emplyeephoto)
        {
            var employee = new Employee();
            //employee.FirstName = newemployee.FirstName;
            _mapper.Map(newemployee, employee);

            if (emplyeephoto != null && emplyeephoto.Length > 0)
            {
                var fileName = "EmployeePhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + emplyeephoto.FileName;
                var path = _configuration["FileSettings:UploadFolder"]!;
                var filepath = Path.Combine(path, fileName);
                var stream = new FileStream(filepath, FileMode.Create);
                await emplyeephoto.CopyToAsync(stream);
                //stream.Close();
                employee.Photo = fileName;

            }
            var statusId = await _context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmployeeStatus" && x.Code=="Active").FirstOrDefaultAsync();
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            employee.CreatedById = Userid;
            employee.CreatedOn = DateTime.Now;
            employee.StatusId = statusId.Id;
            // if (ModelState.IsValid)
            //  {
            _context.Add(employee);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
            //   }
            ViewData["DisabilityId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "DisabilityTypes"), "Id", "Description",employee.DisabilityId);
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", employee.BankId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);

            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", employee.GenderId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", employee.BankId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpNo,FirstName,MiddleName,LastName,PhoneNumber,EmailAddress,Country,DateOfBirth,Address,Department,Designation,CreatedById,CreatedOn,ModifiedBy,ModifiedOn")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", employee.BankId);
            ViewData["EmploymentTermsId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "EmploymentTerms"), "Id", "Description", employee.EmploymentTermsId);
            ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", employee.GenderId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
            ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync(Userid);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
