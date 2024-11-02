using HR_Management.Data;
using HR_Management.Models;
using HR_Management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HR_Management.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfilesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActionResult> Index()
        {
            var task = new ProfileViewModel();
            var roles =  await _dbContext.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles,"Id","Name");

            var systemtasks = await _dbContext.SystemProfiles
                .Include("Children.Children.Children")
                .OrderBy(x=>x.Order)
                //.Where(t=>t.ProfileId ==null)
                .ToListAsync();
            ViewBag.Tasks = new SelectList(systemtasks,"Id","Name");
            return View(task);
        }
        public async Task<ActionResult> AssignRights(ProfileViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = new RoleProfile
            {
                TaskId = vm.TaskId,
                RoleId = vm.RoleId,
            };
            _dbContext.RoleProfiles.Add(role);
            await _dbContext.SaveChangesAsync(userId);
            return RedirectToAction("Index");
        
        }
        [HttpGet]
        public async Task<ActionResult> UserRights(string id)
        {
            var task = new ProfileViewModel();
            task.RoleId = id;

            task.Profiles = await _dbContext.SystemProfiles
                .Include(s => s.Profile)
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();

            task.RolesRightsIds = await _dbContext.RoleProfiles.Where(x => x.RoleId == id).Select(r => r.TaskId).ToListAsync();

            return View(task);
        }
        [HttpPost]
        public async Task<ActionResult> UserGroupRights (string id, ProfileViewModel vm)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allrights = await _dbContext.RoleProfiles.Where(x => x.RoleId == id).ToListAsync();
            _dbContext.RoleProfiles.RemoveRange(allrights);
            await _dbContext.SaveChangesAsync(Userid);
            foreach (var taskId in vm.Ids)
            {
                var role = new RoleProfile
                {
                    TaskId = taskId,
                    RoleId = id,
                };
                _dbContext.RoleProfiles.Add(role);
                await _dbContext.SaveChangesAsync(Userid);
            }
            return RedirectToAction("Index");
        }
    }
}
