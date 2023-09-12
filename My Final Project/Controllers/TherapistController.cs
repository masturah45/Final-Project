﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace My_Final_Project.Controllers
{
    public class TherapistController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ITherapistService _therapistService;
        private readonly ITherapistIssuesService _therapistIssuesService;
        private readonly IIssuesService _issuesService;

        public TherapistController(IClientService clientService, ITherapistService therapistService, ITherapistIssuesService therapistIssuesService, IIssuesService issuesService)
        {
            _clientService = clientService;
            _therapistService = therapistService;
            _therapistIssuesService = therapistIssuesService;
            _issuesService = issuesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var issues = await _issuesService.GetAllIssues();
            var selectedIissues = new SelectList(issues.Data, "Id", "Name");
            var requestModel = new CreateTherapistRequestModel
            {
                Issues = selectedIissues,
            };
            return View(requestModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTherapistRequestModel model)
        {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Therapist Created error";
                return View(model);
                }
                var responde = await _therapistService.Create(model);
                TempData["success"] = "Therapist Created Successfully";
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var therapist = await _therapistService.GetTherapist(id);
            if (therapist == null)
            {
                ViewBag.Error = "Therapist doesnt exist";
            }
            return View(therapist.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateTherapistRequestModel model)
        {
            var t = await _therapistService.Update(id, model);
            TempData["success"] = "Therapist edited successfully";
            return RedirectToAction("TherapistBoard", "User");
        }

        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var therapist = await _therapistService.GetTherapist(id);
            if (therapist == null)
            {
                ViewBag.Error = "doesnt exist";
            }
            return View(therapist.Data);
        }

        //[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _therapistService.Delete(id);
            TempData["error"] = "Therapist deleted successfully";
            return RedirectToAction("GetAll", "Therapist");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var therapists = await _therapistService.GetAll();
            TempData["success"] = "All Therapist";
            return View(therapists);
        }
        [HttpGet]
        public async Task<IActionResult> Profile(Guid id)
        {
            var therapist = await _therapistService.GetTherapist(id);
            TempData["success"] = "Therapist Profile";
            if (therapist == null)
            {
                return NotFound();
            }
            return View(therapist.Data);
        }
        [HttpGet]

        public async Task<IActionResult> ViewUnApprovedTherapist()
        {
            var therapist = await _therapistService.ViewUnapprovedTherapist();
            if (therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();

        }
        [HttpGet]
        public async Task<IActionResult> ViewApprovedTherapist()
        {
            var therapist = await _therapistService.ViewapprovedTherapist();
            if (therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> ViewRejectedTherapist()
        {
            var therapist = await _therapistService.ViewRejectedTherapist();
            if(therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailable()
        {
            var therapist = await _therapistService.GetAvailableTherapist();
            if (therapist.Status == true)
            {
                return View(therapist.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> RejectTherapist(Guid id)
        {
            var therapist = await _therapistService.RejectapprovedTherapist(id);
            return RedirectToAction("ViewUnApprovedTherapist");
        }

        [HttpGet]

        public async Task<IActionResult> ApprovedTherapist(Guid id)
        {
            var therapist = await _therapistService.Approve(id);
            return RedirectToAction("ViewUnApprovedTherapist");
        }

        [HttpGet]
        public async Task<IActionResult> GetTherapistByIssues(Guid IssueId)
        {
            var therapistIssues = await _therapistIssuesService.GetTherapistByIssues(IssueId);
            return View(therapistIssues);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTherapist()
        {
            var therapists = await _therapistService.GetAll();
            TempData["success"] = "All Therapist";
            return View(therapists);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAvailableTherapist(bool isAvailable, Guid id)
        //{
        //    var getAvailableTherapist = await _therapistService.GetTherapistAvailabilityStatus(isAvailable, id);
        //    return View(getAvailableTherapist);
        //}
    }
}
