using HR_Management.Data;
using HR_Management.Models;
using HR_Management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            //var userdetails = new ApplicationUser();
            var users = await _dbContext.Users.Include(x=> x.Role).ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_dbContext.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            //ApplicationUser user = new ApplicationUser();
            ApplicationUser user = new ();
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.NationalId = model.NationalId;
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = true;
            user.CreatedOn = DateTime.Now;
            user.CreatedById = "TIGER"; // Linked with logined user
            user.RoleId = model.RoleId;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
            return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            ViewData["RoleId"] = new SelectList(_dbContext.Roles, "Id", "Name", model.RoleId);
        }
    }
}
