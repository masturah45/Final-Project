using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUser(Guid id);
        Task<User> Get(string name);
        Task<User> Get(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
