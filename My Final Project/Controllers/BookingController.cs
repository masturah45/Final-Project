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
        private readonly IUserService _userService;

        public BookingController(IBookingService bookingService, ITherapistService therapist, IUserService userService)
        {
            _bookingService = bookingService;
            _therapist = therapist;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid TherapistId)
        {
            var requestmodel = new CreateBookingRequestModel
            {
                TherapistId = TherapistId,
                //AppointmentDateTime = 
            
                
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
                return RedirectToAction("ClientBoard", "User");
            }   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Therapistid)
        {
            var booking = await _bookingService.GetBooking(Therapistid);
            if (booking == null)
            {
                ViewBag.Error = "Booking doesnt exist";
            }
            return View(booking.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Guid Therapistid, UpdateBookingRequestModel model)
        {
            await _bookingService.Update(Therapistid, model);
            TempData["success"] = "Booking updated successfully";
            return RedirectToAction("GetAll", "Booking");
        }

        public async Task<IActionResult> Delete(Guid Therapistid)
        {

            var booking = await _bookingService.GetBooking(Therapistid);
            if (booking == null)
            {
                ViewBag.Error = "doesnt exist";
            }
            return View(booking.Data);
        }
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bookingService.Delete(id);
            TempData["error"] = "It has successfully been unbooked";
            return RedirectToAction("GetAll", "Booking");
        }
        [HttpGet]
        //[Route("Booking/Profile/{TherapistId}")]
        public async Task<IActionResult> Profile([FromQuery] Guid TherapistId)
        {
            //var user = _userService.Get(User.Identity.Name).Id;
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

        public async Task<IActionResult> Cancel(Guid ClientId)
        {
            var booking = await _bookingService.CancelBooking(ClientId);
            if(booking == null)
            {
                TempData["success"] = "Booking has already been cancelled";
            }
            return RedirectToAction("GetAll", "Booking");
        }
    }
}
