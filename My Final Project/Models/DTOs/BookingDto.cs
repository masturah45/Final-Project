using Microsoft.AspNetCore.Mvc.Rendering;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Models.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid TherapistId { get; set; }
        public Guid ClientId { get; set; }
        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public SelectList Therapists { get; set; }
        public bool IsApproved { get; set; } = false;
    }

    public class CreateBookingRequestModel
    {
        public Guid TherapistId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }

    public class UpdateBookingRequestModel
    {
        public string TherapistNmae { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
