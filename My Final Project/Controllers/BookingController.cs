using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using My_Final_Project.Implementations.Services;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using System.Security.Claims;

namespace My_Final_Project.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITherapistService  _therapist;

        public BookingController(IBookingService bookingService, ITherapistService therapist)
        {
            _bookingService = bookingService;
            _therapist = therapist;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid TherapistId)
        {
            var requestmodel = new CreateBookingRequestModel
            {
                TherapistId = TherapistId
            };

            return View(requestmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingRequestModel model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var booking = await _bookingService.Create(model, Guid.Parse(userId));
            TempData["success"] = "Booking Created Sucessfully";
            if (booking.Status == true)
            {
                return RedirectToAction("Create", "Booking");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var booking = await _bookingService.GetBooking(id);
            if (booking == null)
            {
                ViewBag.Error = "Booking doesnt exist";
            }
            return View(booking.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Guid id, UpdateBookingRequestModel model)
        {
            await _bookingService.Update(id, model);
            TempData["success"] = "Booking updated successfully";
            return RedirectToAction("GetAll", "Booking");
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var booking = await _bookingService.GetBooking(id);
            if (booking == null)
            {
                ViewBag.Error = "doesnt exist";
            }
            return View(booking.Data);
        }
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bookingService.Delete(id);
            TempData["error"] = "Booking deleted Sucessfully";
            return RedirectToAction("GetAll", "Booking");
        }
        [HttpGet]
        public async Task<IActionResult> Profile(Guid TherapistId)
        {
            var booking = await _bookingService.GetBooking(TherapistId);
            TempData["success"] = "Booking Profile";
            if (booking == null)
            {
                ViewBag.Error = "Booking doesnt exist";
            }
            return View(booking.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAll();
            return View(bookings);
        }
    }
}
