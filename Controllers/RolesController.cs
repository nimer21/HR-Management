using HR_Management.Data;
using HR_Management.Models;
using HR_Management.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HR_Management.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public RolesController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            IdentityRole role = new IdentityRole();
            //IdentityRole role = new();
            role.Name = model.RoleName;

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = new RolesViewModel();
            var result = await _roleManager.FindByIdAsync(id);
            role.RoleName = result.Name;
            role.Id = result.Id;
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RolesViewModel model)
        {
            var checkifexist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!checkifexist) { 
            var result = await _roleManager.FindByIdAsync(id);
            //IdentityRole role = new();
            result.Name = model.RoleName;

            var finalresult = await _roleManager.UpdateAsync(result);
            if (finalresult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            }
            //return View(model);
            return RedirectToAction("Index");
        }
    }
}
