using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> GetUser(string id);
        Task<User> Get(string name);
        Task<User> Get(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
