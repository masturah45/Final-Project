using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategory(Guid id);
        Task<Category> GetCategory (Expression<Func<Category, bool>> expression);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
