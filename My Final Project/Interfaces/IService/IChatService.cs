using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface IChatService
    {
        Task<BaseResponse<ChatDto>> CreateChat(CreateChatRequestModel model, Guid loginId, Guid senderId, string role);
        Task<BaseResponse<List<ChatDto>>> GetAllChatFromASender(Guid loginId, Guid senderId, string role);
        Task<BaseResponse<ChatDto>> MarkAllChatAsRead(Guid clientId, Guid therapistId); 
        //Task<BaseResponse<List<ChatDto>>> GetAllUnSeenChat(Guid therapistId);

    }
}
