using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.Runtime.Serialization.Json;
using System.Security.Claims;

namespace My_Final_Project.Controllers
{
    public class ChatController : Controller
    {

        private readonly IConfiguration _config;
        private readonly IChatService _chatService;
        private readonly IClientService _clientService;
        private readonly ITherapistService _therapistService;

        public ChatController(IConfiguration config, IChatService chatService, IClientService clientService, ITherapistService therapistService)
        {
            _config = config;
            _chatService = chatService;
            _clientService = clientService;
            _therapistService = therapistService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChatRequestModel model, Guid senderId, string role)
        {
            var Id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var chat = await _chatService.CreateChat(model, Guid.Parse(Id), senderId, role);
            return RedirectToAction("Chat" ,"GetAllChats");
        }

        [HttpGet]  
        public async Task<IActionResult> GetAllChats()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if(role == "Therapist")
            {
                var clients = await _clientService.GetAllClientByChat();
                return View(clients);
            }
            else if(role == "Client")
            {
                var therapists = await _therapistService.GetAllTherapistByChat();
                return View(therapists);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Chat(Guid id, CreateChatRequestModel model)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (HttpContext.Request.Method == "POST")
            {
                var Id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var chat = await _chatService.CreateChat(model, Guid.Parse(Id), id, role);
            }
            var loginid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var chats = await _chatService.GetAllChatFromASender(Guid.Parse(loginid), id, role);
            return View(chats.Data);
        }
        //[HttpPost]
        //public async Task<IActionResult> Chat(CreateChatRequestModel model,[FromRoute] Guid senderId)
        //{

        //    var Id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var chat = await _chatService.CreateChat(model, Guid.Parse(Id), senderId);
        //    return View();

        //}


    }
}
