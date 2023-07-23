using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            var role = await _roleService.Create(model);
            if (role.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var roled = await _roleService.GetRole(id);
            if (roled == null)
            {
                ViewBag.Error = "doesnt exist";
            }
            return View(roled.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _roleService.Delete(id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRole();
            return View(roles.Data);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var ho = await _roleService.GetRole(id);
            return View(ho.Data);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var roled = await _roleService.GetRole(id);
            if (roled == null)
            {
                ViewBag.Error = "Doesnt exist";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateRoleRequestModel model)
        {
            var roled = await _roleService.Update(id, model);
            return View(roled);
        }
    }
}
