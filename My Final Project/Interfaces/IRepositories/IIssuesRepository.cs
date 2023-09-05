using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IIssuesRepository : IBaseRepository<Issue>
    {
        Task<Issue> GetIssues(Guid id);
        Task<Issue> GetIssues(Expression<Func<Issue, bool>> expression);
        Task<IEnumerable<Issue>> GetAllIssues();
    }
}
