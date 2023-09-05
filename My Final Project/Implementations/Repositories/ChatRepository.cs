using Microsoft.EntityFrameworkCore;
using My_Final_Project.ApplicationContext;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Implementations.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
         
        public async Task<List<Chat>> GetAllChatFromASender(Guid loginId, Guid senderId)
        {
            return await _context.Chats
            .Where(x => x.SenderId == loginId && x.RecieverId == senderId || x.RecieverId == senderId && x.SenderId == loginId)
            .OrderBy(x => x.DateCreated)
            .ToListAsync();
        }

        public async Task<List<Chat>> GetAllUnSeenChat(Guid loginId)
        {
            return await _context.Chats.Where(x => x.RecieverId == loginId && x.Seen == false).ToListAsync();
        }

        public async Task<List<Chat>> GetAllUnSeenChat(Guid clientId, Guid therapistId)
        {
            throw new NotImplementedException();
        }
    }
}
