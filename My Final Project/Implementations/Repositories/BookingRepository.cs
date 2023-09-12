using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<Booking> CancelBooking()
        //{
        //    return await _context.Bookings.Where(x.I)
        //}

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Booking> GetBooking(Guid TherapistId)
        {
            return await _context.Bookings.Where(x => !x.IsDeleted).FirstOrDefaultAsync(a => a.TherapistId == TherapistId);
        }

        public async Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression)
        {
            return await _context.Bookings.SingleOrDefaultAsync(expression);
        }

        public async Task<Booking> GetBookingByClientId(Guid clientId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(a => a.ClientId == clientId);
        }

        public async Task<Booking> GetBookingByTherapistId(Guid therapistId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(a => a.TherapistId == therapistId);
        }
    }
}
