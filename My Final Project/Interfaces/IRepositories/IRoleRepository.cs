﻿using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetRole(Guid id);
        Task<Role> GetRole(Expression<Func<Role, bool>> expression);
        Task <IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleByName(string name);

    }
}
