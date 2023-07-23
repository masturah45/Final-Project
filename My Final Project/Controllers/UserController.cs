using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using System.Security.Claims;

namespace My_Final_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInUserRequestModel model)
        {
            var user = await _userService.Login(model);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier , user.Data.Id.ToString()),
                     new Claim(ClaimTypes.Email, user.Data.Email),
                     new Claim(ClaimTypes.Name, user.Data.FirstName + " " + user.Data.LastName),


                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                TempData["Success"] = "Successfully LogIn";
                if (user.Status == true)
                {
                    if (user.Data.Roles.Select(r => r.Name).Contains("SuperAdmin"))
                        return RedirectToAction("GetAllSuperAdmin", "SuperAdmin");

                    else if (user.Data.Roles.Select(r => r.Name).Contains("Client"))
                        return RedirectToAction("GetAllClient", "Client");
                    else if (user.Data.Roles.Select(r => r.Name).Contains("Therapist"))
                        return RedirectToAction("GetAllTherapist", "Therapist");
                }
            }
            ViewBag.error = "Invalid Email or password entered";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult SuperAdminBoard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ClientBoard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TherapistBoard()
        {
            return View();
        }
    }

}
