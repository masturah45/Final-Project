﻿using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Implementations.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(r => r.User)
            .ToListAsync();
        }

        public async Task<Role> GetRole(Guid id)
        {
            return await _context.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Role> GetRole(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
