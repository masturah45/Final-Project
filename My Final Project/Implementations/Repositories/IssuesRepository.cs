using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class IssuesRepository : BaseRepository, IIssuesRepository
    {
        public IssuesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Issue>> GetAllIssues()
        {
            return await _context.Issues.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Issue> GetIssues(Guid id)
        {
            return await _context.Issues.Where(x => !x.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Issue> GetIssues(Expression<Func<Issue, bool>> expression)
        {
            return await _context.Issues.Where(x => !x.IsDeleted).SingleOrDefaultAsync(expression);
        }
    }
}
