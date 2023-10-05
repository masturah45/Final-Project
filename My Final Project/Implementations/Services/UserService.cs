using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System;
using System.Data;
using System.Text.Json.Serialization;

namespace My_Final_Project.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITherapistRepository _theraphy;
        private readonly IClientRepository _client;
        private readonly ISuperAdminRepository _superAdmin;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, ITherapistRepository theraphy, IClientRepository client, ISuperAdminRepository superAdmin)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _theraphy = theraphy;
            _client = client;
            _superAdmin = superAdmin;
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
                    Id = user.Id,
                    //Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,

                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = user.Id,
                        //Id = Guid.NewGuid(),
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
            var userlogin = await _userRepository.Get(a => a.Email == model.Email && a.Password == model.Password && a.IsDeleted == false);
           
            dynamic us = null;
            if (userlogin.Client != null)
            {
                us = await _client.GetClient(a => a.UserId == userlogin.Id);
            }
            if (userlogin.Therapist != null)
            {
                us = await _theraphy.GetTherapist(a => a.UserId == userlogin.Id);
            }
            if (userlogin.SuperAdmin != null)
            {
                us = await _superAdmin.GetSuperAdmin(a => a.UserId == userlogin.Id);
            }
            if (userlogin != null)
            {

                return new BaseResponse<UserDto>
                {
                    Message = "Login Successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = userlogin.Id,
                        FirstName = userlogin.FirstName,
                        LastName = userlogin.LastName,
                        Email = userlogin.Email,
                        PhoneNumber = userlogin.PhoneNumber,

                        Roles = userlogin.UserRoles.Select(a => new RoleDto
                        {
                            Id = Guid.NewGuid(),
                            Name = a.Role.Name,
                            Description = a.Role.Description,
                        }).ToList(),
                    }

                };

            }
            else
            {

                return new BaseResponse<UserDto>
                {
                    Message = "incorrect details",
                    Status = false,
                };
               
            }

        }

        public async Task<BaseResponse<UserDto>> Get(string name)
        {
            var user = await _userRepository.Get(name);
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
                    Id = user.Id,
                    //Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,

                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = user.Id,
                        //Id = Guid.NewGuid(),
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };

        }
    }
}
