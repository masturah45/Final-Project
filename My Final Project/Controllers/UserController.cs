﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using System.Security.Claims;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Implementations.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ITherapistService _therapistService;

        private readonly IUserService _userService;
        private readonly UserManager<User> _manager;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserService userService, ITherapistService therapistService, UserManager<User> manager, SignInManager<User> signInManager)
        {
            _userService = userService;
            _therapistService = therapistService;
            _manager = manager;
            _signInManager = signInManager;
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
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
            /*if (user.Data != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier , user.Data.Id.ToString()),
                     new Claim(ClaimTypes.GivenName , user.Data.Id2.ToString()),
                     new Claim(ClaimTypes.Email, user.Data.Email),
                     new Claim(ClaimTypes.HomePhone, user.Data.PhoneNumber),
                     new Claim(ClaimTypes.Name, user.Data.FirstName),
                     new Claim(ClaimTypes.Name, user.Data.LastName),


                };
                foreach (var item in user.Data.Roles)
                {
                    claims.Add(new Claim("role", item.Name));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
                TempData["Success"] = "Successfully LogIn";
                if (user.Status == true)
                {
                    return RedirectToAction("DashBoard", "Home");
                }
            }*/
            if (result.Succeeded)
            {
                return  RedirectToAction("DashBoard","Home");
            }
            ViewBag.error = "Invalid Email or password entered";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SuperAdminBoard()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ClientBoard()
        {
            var therapists = await _therapistService.GetAll();
            return View(therapists);
            //return View();
        }
        [HttpGet]
        public IActionResult TherapistBoard()
        {
            return View();
        }
    }

}
