using My_Final_Project.Models.Entities;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IChatRepository : IBaseRepository<Chat>
    { 
        Task<List<Chat>> GetAllUnSeenChat(Guid therapistId);
        Task<List<Chat>> GetAllUnSeenChat(Guid clientId, Guid therapistId);
        Task<List<Chat>> GetAllChatFromASender(Guid loginId, Guid senderId);
    }
}
