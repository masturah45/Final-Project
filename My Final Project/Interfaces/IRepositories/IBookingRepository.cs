using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetBooking(Guid TherapistId);
        //Task<Booking> CancelBooking();
        Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression);
        Task<IEnumerable<Booking>>GetAll();
        Task<Booking> GetBookingByTherapistId(Guid therapistId);
        Task<Booking> GetBookingByClientId(Guid clientId);
    }
}
