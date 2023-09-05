using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface ITherapistIssuesService
    {
        Task<IEnumerable<TherapistIssuesDto>> GetAll();
        Task<BaseResponse<IEnumerable<TherapistDto>>> GetTherapistByIssues(Guid IssuesId);
        Task<BaseResponse<TherapistIssuesDto>> GetTherapistIssues(Guid id);
    }
}
