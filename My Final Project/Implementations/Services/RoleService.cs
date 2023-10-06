using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace My_Final_Project.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get<Role>(b => b.Name == model.Name);
            if (roleExist != null) return new BaseResponse<RoleDto>
            {
                Message = "Role already exist",
                Status = false,
            };

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                IsDeleted = false,
            };

            await _roleRepository.Add(role);

            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = "Created Successfully",
                Data = new RoleDto
                {
                    Name = model.Name
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> Delete(Guid id)
        {
            var role = await _roleRepository.Get<Role>(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "Role Not Found",
                Status = false,
            };

            //_roleRepository.Save()

            return new BaseResponse<RoleDto>
            {
                Message = "Deleted Successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRole(Guid id)
        {
            var role = await _roleRepository.Get<Role>(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "Role Not Found",
                Status = false,
            };

            return new BaseResponse<RoleDto>
            {
                Message = "Successfull",
                Status = true,
                Data = new RoleDto
                {
                    Id = Guid.NewGuid(),
                    Name = role.Name,
                    Description = role.Description,
                    Users = role.UserRoles.Select(v => new UserDto
                    {
                        FirstName = v.User.FirstName,
                        LastName = v.User.LastName,
                        Email = v.User.Email,
                        Password = v.User.Password
                    }).ToList(),
                },
            };
        }

        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRole()
        {
            var roles = await _roleRepository.GetAll<Role>();
            var listOfRoles = roles.ToList().Select(b => new RoleDto
            {
                Id = Guid.NewGuid(),
                Name = b.Name,
                Description = b.Description,
                Users = b.UserRoles.Select(a => new UserDto
                {
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    Email = a.User.Email,
                    Password = a.User.Password
                }).ToList(),
            });
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfRoles,
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRoleByName(string name)
        {
            var role =  _roleRepository.GetRoleByName(name);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "role not found",
                Status = false,
            };
            return new BaseResponse<RoleDto>
            {
                Message = "Successfully",
                Status = true,
                Data = new RoleDto
                {
                    Id = Guid.NewGuid(),
                    Name = role.Name,
                    Description = role.Description,
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> Update(Guid id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get<Role>(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "role not found",
                Status = false,
            };
            id = Guid.NewGuid();
            role.Name = model.Name;
            role.Description = model.Description;
            await _roleRepository.Update(role);

            return new BaseResponse<RoleDto>
            {
                Message = "Successfully Updayted",
                Status = true,
                Data = new RoleDto
                {
                    Name = model.Name
                }
            };
        }

    }
}
