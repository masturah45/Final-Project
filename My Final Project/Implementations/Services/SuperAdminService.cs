using Microsoft.AspNetCore.Identity;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Implementations.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superadminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly UserManager<User> _userManager;
        public SuperAdminService(ISuperAdminRepository superadminRepository, IUserRepository userRepository, IRoleRepository roleRepository, UserManager<User> userManager)
        {
            _superadminRepository = superadminRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse<SuperAdminDto>> Create(CreateSuperAdminRequestModel model)
        {
            var superadminExist = await _userRepository.Get(a => a.Email == model.Email);
            if (superadminExist != null) return new BaseResponse<SuperAdminDto>
            {
                Message = "User already exist",
                Status = false,
            };

            var role = await _roleRepository.Get<Role>(b => b.Name == "SuperAdmin");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,

                UserRoles = new List<UserRole>()
            };

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = Guid.NewGuid(),
                Role = role,
                User = user,
            };
            user.UserRoles.Add(userRole);
            await _userManager.CreateAsync(user);

            var superAdmin = new SuperAdmin
            {
                User = user,
                UserId = Guid.NewGuid().ToString(),
            };

            await _superadminRepository.Add(superAdmin);

            return new BaseResponse<SuperAdminDto>
            {
                Message = "SuperAdmin created successfully",
                Status = true,
                Data = new SuperAdminDto
                {
                    FirstName = superAdmin.User.FirstName,
                    LastName = superAdmin.User.LastName,
                    Email = superAdmin.User.Email,
                }
            };
        }

        public async Task<BaseResponse<SuperAdminDto>> Delete(Guid id)
        {
            var superadmin = await _superadminRepository.GetSuperAdmin(id);
            if (superadmin == null) return new BaseResponse<SuperAdminDto>
            {
                Message = "SuperAdmin Not Found",
                Status = false,
            };

            _superadminRepository.save();

            return new BaseResponse<SuperAdminDto>
            {
                Message = "Delete Successful",
                Status = true,
            };
        }

        public async Task<IEnumerable<SuperAdminDto>> GetAllSuperAdmin()
        {
            var superadmins = await _superadminRepository.GetAll<SuperAdmin>();
            var listOfSuperAdmins = superadmins.Select(a => new SuperAdminDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber,
            }).ToList();
            return listOfSuperAdmins;
        }

        public async Task<BaseResponse<SuperAdminDto>> GetSuperAdmin(Guid id)
        {
            var superAdmin = await _superadminRepository.GetSuperAdmin(id);
            if (superAdmin == null) return new BaseResponse<SuperAdminDto>
            {
                Message = "SuperAdmin not found",
                Status = false,
            };

            return new BaseResponse<SuperAdminDto>
            {
                Message = "Successful",
                Status = true,
                Data = new SuperAdminDto
                {
                    Id = superAdmin.Id,
                    FirstName = superAdmin.User.FirstName,
                    LastName = superAdmin.User.LastName,
                    PhoneNumber = superAdmin.User.PhoneNumber,
                    Email = superAdmin.User.Email,
                }
            };
        }

        public async Task<BaseResponse<SuperAdminDto>> Update(Guid id, UpdateSuperAdminRequestModel model)
        {
            var superadmin = await _superadminRepository.GetSuperAdmin(id);
            if (superadmin == null) return new BaseResponse<SuperAdminDto>
            {
                Message = "superadmin not found",
                Status = false,
            };

            superadmin.User.FirstName = model.FirstName;
            superadmin.User.LastName = model.LastName;
            superadmin.User.Email = model.Password;
            superadmin.User.PhoneNumber = model.PhoneNumber;
            superadmin.DateCreated = DateTime.Now;
            superadmin.DateUpdated = DateTime.Now;
            superadmin.IsDeleted = false;

            await _superadminRepository.Update(superadmin);

            return new BaseResponse<SuperAdminDto>
            {
                Message = "Successful",
                Status = true,
                Data = new SuperAdminDto
                {
                    FirstName = superadmin.User.FirstName,
                    LastName = superadmin.User.LastName,
                }
            };

        }
    }
}
