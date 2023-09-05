using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface IIssuesService
    {
        Task<BaseResponse<IssuesDto>> Create(CreateIssuesRequestModel model);
        Task<BaseResponse<IssuesDto>> Update(Guid id, UpdateIssuesRequestModel model);
        Task<BaseResponse<IssuesDto>> GetIssues(Guid id);
        Task<BaseResponse<IssuesDto>> Delete(Guid id);
        Task<BaseResponse<IEnumerable<IssuesDto>>> GetAllIssues();
    }
}
