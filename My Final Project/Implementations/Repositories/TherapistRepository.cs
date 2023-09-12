using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using My_Final_Project.Models.Enum;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class TherapistRepository : BaseRepository<Therapist>, ITherapistRepository
    {

        public TherapistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Therapist> CheckIfExist(string email)
        {
            return await _context.Therapists.Where(e => e.User.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Therapist>> GetAvailableTherapist()
        {
            return await _context.Therapists.Where(x => x.IsAvalaible == true).ToListAsync();
        }

        public async Task<IEnumerable<Therapist>> GetAllTherapist()
        {
            return await _context.Therapists
                .Include(x => x.TherapistIssues)
                .ThenInclude(x => x.Issue)
                .Include(a => a.User).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Therapist>> GetAllTherapistByChat()
        {
            return await _context.Therapists.Include(a => a.User).Where(x => !x.IsDeleted && !x.IsActive ).ToListAsync();
        }

        public async Task<IEnumerable<Therapist>> GetApprovedTherapist()
        {
            return await _context.Therapists.Where(x => x.Status == Aprrove.Approved && !x.IsDeleted).Include(a => a.User).ToListAsync();
        }

        public async Task<Therapist> GetTherapist(Guid id)
        {
            return await _context.Therapists.Include(a => a.User).Where(x => !x.IsDeleted && !x.IsActive).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Therapist> GetTherapist(Expression<Func<Therapist, bool>> expression)
        {
            return await _context.Therapists.Include(a => a.User).Where(x => !x.IsDeleted && !x.IsActive).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Therapist>> GetUnapprovedTherapist()
        {
            return await _context.Therapists.Where(x => x.Status == Aprrove.Pending && !x.IsDeleted).Include(a => a.User).ToListAsync();
        }

        public async Task<IEnumerable<Therapist>> GetRejectedTherapist()
        {
            return await _context.Therapists.Where(x => x.Status == Aprrove.Rejected && !x.IsDeleted).Include(a => a.User).ToListAsync();   
        }
    }
}
