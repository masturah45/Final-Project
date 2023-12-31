﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using System.Security.Claims;

namespace My_Final_Project.Controllers
{
    public class ClientController : Controller
    {
        private readonly ITherapistService _therapistService;
        private readonly IClientService _clientService;

        public ClientController(ITherapistService therapistService, IClientService clientService)
        {
            _therapistService = therapistService;
            _clientService = clientService;
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
        public async Task<IActionResult> Create(CreateClientRequestModel model)
        {
                if(ModelState.IsValid)
                {
                    TempData["error"] = "Client Created error";
                    return View(model);
                }
                await _clientService.Create(model);
                TempData["success"] = "Client Created Successfully";
                return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                ViewBag.Error = "Client doesnt exist";
            }
            return View(client.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateClientRequestModel model)
        {


            await _clientService.Update(id, model);
            TempData["success"] = "Client updated successfully";
            return RedirectToAction("Profile", "Client");
        }

        //[Authorize(Roles = "Client")]
        //[HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                ViewBag.Error = "doesnt exist";
            }
            return View(client.Data);
        }
        //[HttpGet]

        //[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _clientService.Delete(id);
            TempData["error"] = "Client deleted successfully";
            return RedirectToAction("GetAll", "Client");
        }
        [HttpGet]
        public async Task<IActionResult> Profile(Guid id)
        {
            var client = await _clientService.GetClient(id);
            TempData["sucess"] = "Client Profile";
            if (client == null)
            {
                ViewBag.Error = "Client doesnt exist";
            }
            return View(client.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAll();
            TempData["success"] = "All Client";
            return View(clients);
        }
    }
}
