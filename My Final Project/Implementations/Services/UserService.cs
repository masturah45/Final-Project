using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.Data;

namespace My_Final_Project.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse<UserDto>> AssignRole(Guid id, List<int> roleIds)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "user not found",
                Status = false,
            };
            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Successfully",
            };
        }

        public async Task<BaseResponse<UserDto>> AssignTherapistRole(Guid id, string name)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "user not found",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Therapist");

            user.UserRoles.Add(new UserRole
            {
                RoleId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            });

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Successfully",
            };
        }

        public async Task<BaseResponse<UserDto>> GetUsers(Guid id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "User not found",
                Status = false,
            };

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Successfully",
                Data = new UserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,

                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = Guid.NewGuid(),
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            var listOfUsers = users.ToList().Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });

            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "success",
                Status = true,
                Data = listOfUsers,
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LogInUserRequestModel model)
        {
            var user = await _userRepository.Get(a => a.Email == model.Email);
            if (user == null || user.Email != model.Email && user.Password != model.Password) return new BaseResponse<UserDto>
            {
                Message = "incorrect details",
                Status = false,
            };

            return new BaseResponse<UserDto>
            {
                Message = "Login Successful",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = Guid.NewGuid(),
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };
        }
    }
}
