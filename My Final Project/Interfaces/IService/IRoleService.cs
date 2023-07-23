using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> Update(Guid id, UpdateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> GetRole(Guid id);
        Task<BaseResponse<RoleDto>> Delete(Guid id);
        Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRole();
        Task<BaseResponse<RoleDto>> GetRoleByName(string name);

    }
}
