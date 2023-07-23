using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Controllers
{
    public class TherapistController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ITherapistService _therapistService;

        public TherapistController(IClientService clientService, ITherapistService therapistService)
        {
            _clientService = clientService;
            _therapistService = therapistService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTherapistRequestModel model)
        {
            await _therapistService.Create(model);
            ViewBag.Message = "Therapist created successfully";
            TempData["Message"] = "Therapist created successfully";
            return RedirectToAction("LogIn", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var therapist = await _therapistService.GetTherapist(id);
            if (therapist == null)
            {
                ViewBag.Error = "Therapist doesnt exist";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateTherapistRequestModel model)
        {
            var t = await _therapistService.Update(id, model);
            ViewBag.Message = "Therapist edited successfully";
            TempData["Message"] = "Therapist edited successfully";
            return RedirectToAction("GetAllTherapist", "Therapist");
        }

        [Authorize(Roles = "Therapist")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var therapist = await _therapistService.GetTherapist(id);
            if (therapist == null)
            {
                ViewBag.Error = " Therapist cannot be deleted";
            }
            return View(therapist);
        }

        [HttpPost, ActionName("DeleteTherapist")]
        public async Task<IActionResult> DeleteTherapist(Guid id)
        {
            await _therapistService.Delete(id);
            ViewBag.Message = "Therapist deleted successfully";
            return RedirectToAction("GetAllTherapist", "Therapist");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var therapists = await _therapistService.GetAllTherapist();
            return View(therapists);
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var therapist = await _therapistService.GetTherapist(id);
            if (therapist == null)
            {
                return NotFound();
            }
            return View(therapist.Data);
        }
        [HttpGet]

        public async Task<IActionResult> ViewUnapprovedTherapist()
        {
            var therapist = await _therapistService.ViewUnapprovedTherapist();
            if(therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();

        }
        [HttpGet]
        public async Task<IActionResult> ViewapprovedTherapist()
        {
            var therapist = await _therapistService.ViewapprovedTherapist();
            if(therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> RemovedApprovedTherapist(Guid id)
        {
            var therapist = await _therapistService.GetTherapist(id);
            if(therapist.Status == true)
            {
                return View(therapist);
            }
            return BadRequest();
        }
    }
}
