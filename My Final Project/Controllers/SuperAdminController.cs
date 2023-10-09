using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using System.Security.Claims;

namespace My_Final_Project.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly ITherapistService _therapistService;
        private readonly ISuperAdminService _superAdminService;

        public SuperAdminController(ITherapistService therapistService, ISuperAdminService superAdminService)
        {
            _therapistService = therapistService;
            _superAdminService = superAdminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSuperAdminRequestModel model)
        {
            await _superAdminService.Create(model);
            ViewBag.Message = "SuperAdmin created successfully";
            return RedirectToAction("LogIn", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var superAdmin = await _superAdminService.GetSuperAdmin(id);
            if (superAdmin == null)
            {
                ViewBag.Error = "SuperAdmin doesnt exist";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateSuperAdminRequestModel model)
        {
            await _superAdminService.Update(id, model);
            ViewBag.Message = "SuperAdmin edited successfully";
            return RedirectToAction("GetAllSuperAdmin", "Client");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var superAdmin = await _superAdminService.GetSuperAdmin(id);
            if (superAdmin == null)
            {
                ViewBag.Error = "SuperAdmin cannot be deleted";
            }
            return View(superAdmin);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var id = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var superAdmin = await _superAdminService.GetSuperAdmin(id);
            TempData["success"] = "SuperAdmin Profile";
            if (superAdmin == null)
            {
                ViewBag.Error = "SuperAdmin doesnt exist";
            }
            return View(superAdmin.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var superAdmins = await _superAdminService.GetAllSuperAdmin();
            return View(superAdmins);
        }
    }
}
