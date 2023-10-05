using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Interfaces.IService
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> Login(LogInUserRequestModel model);
        Task<BaseResponse<UserDto>> GetUsers(Guid id);
        Task<BaseResponse<IEnumerable<UserDto>>> GetAllUsers();
        Task<BaseResponse<UserDto>> AssignRole(Guid id, List<int> roleIds);
        Task<BaseResponse<UserDto>> AssignTherapistRole(Guid id, string name);
        Task<BaseResponse<UserDto>> Get(string name);
    }
}
