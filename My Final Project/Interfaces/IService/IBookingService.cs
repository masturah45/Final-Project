using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface IBookingService
    {
        Task<BaseResponse<BookingDto>> Create(CreateBookingRequestModel model, Guid clientId);
        Task<BaseResponse<BookingDto>> Update(Guid id, UpdateBookingRequestModel model);
        Task<BaseResponse<BookingDto>> GetBooking(Guid TherapistId);
        Task<BaseResponse<BookingDto>> Delete(Guid id);
        Task<BaseResponse<IEnumerable<BookingDto>>> GetAll();
    }
}
