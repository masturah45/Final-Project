using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            await _clientService.Create(model);
            ViewBag.Message = "Client created successfully";
            return RedirectToAction("LogIn", "User");
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                ViewBag.Error = "Client doesnt exist";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateClientRequestModel model)
        {


            await _clientService.Update(id, model);
            ViewBag.Message = "Client edited successfully";
            return RedirectToAction("GetAllClient", "Client");
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                ViewBag.Error = "Client cannot be deleted";
            }
            return View(client);
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientService.GetClient(id);
            if (client == null)
            {
                ViewBag.Error = "Client doesnt exist";
            }
            return View(client.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = await _clientService.GetAllClient();
            return View(client);
        }
    }
}
