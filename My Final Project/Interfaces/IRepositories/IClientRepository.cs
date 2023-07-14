using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client> GetClient(Guid id);
        Task<Client> GetClient(Expression<Func<Client, bool>> expression);
        Task<IEnumerable<Client>> GetAllClients();
    }
}
