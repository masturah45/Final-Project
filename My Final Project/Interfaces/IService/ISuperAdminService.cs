using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface ISuperAdminService
    {
        Task<BaseResponse<SuperAdminDto>> Create(CreateSuperAdminRequestModel model);
        Task<BaseResponse<SuperAdminDto>> Update(Guid id, UpdateSuperAdminRequestModel model);
        Task<BaseResponse<SuperAdminDto>> GetSuperAdmin(Guid id);
        Task<BaseResponse<SuperAdminDto>> Delete(Guid id);
        Task<IEnumerable<SuperAdminDto>> GetAllSuperAdmin();  
    }
}
