using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface ITherapistService
    {
        Task<BaseResponse<TherapistDto>> Create (CreateTherapistRequestModel model);
        Task<BaseResponse<TherapistDto>> Update(Guid id, UpdateTherapistRequestModel model);
        Task<BaseResponse<TherapistDto>> GetTherapist (Guid id);
        Task<IEnumerable<TherapistDto>> GetAll ();
        Task<BaseResponse<TherapistDto>> Delete(Guid id);
        Task<BaseResponse<IEnumerable<TherapistDto>>> ViewUnapprovedTherapist();
        Task<BaseResponse<IEnumerable<TherapistDto>>> ViewapprovedTherapist();
        Task<BaseResponse<IEnumerable<TherapistDto>>> ViewRejectedTherapist();
        Task<BaseResponse<TherapistDto>> RejectapprovedTherapist(Guid id);
        Task<IEnumerable<TherapistDto>> GetAllAvailableTherapist();
        Task<List<UserDto>> GetAllTherapistByChat();
        Task<BaseResponse<TherapistDto>> Approve(Guid id);
        //Task<BaseResponse<TherapistDto>> GetTherapistAvailabilityStatus(bool isAvailable, Guid id);

    }
}
