using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.Security.Claims;

namespace My_Final_Project.Helper
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private ApplicationDbContext _appliationContext;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        public UserClaimsPrincipalFactory(
        UserManager<User> userManager,
        IOptions<IdentityOptions> optionsAccessor, ApplicationDbContext applicationContext, IUserRepository userRepository, IRoleRepository roleRepository)
            : base(userManager, optionsAccessor)
        {
            _appliationContext = applicationContext;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            //get the data from dbcontext
            var userlogin = await _userRepository.Get(a => a.Email == user.Email && a.IsDeleted == false);

            var identity = await base.GenerateClaimsAsync(user);
            //Get the data from EF core

            var userRole = userlogin.UserRoles.Select(a => new RoleDto
            {
                Id = a.Role.Id,
                Name = a.Role.Name,
                Description = a.Role.Description,
            }).ToList();

            if (userRole != null)
            {
                var claims = new List<Claim>
                {
                     


                };
                foreach (var item in userRole)
                {
                    identity.AddClaim(new Claim("role", item.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Role, item.Name));
                }

                return identity;
            }
            return identity;
        }
    }
}
