﻿using Microsoft.EntityFrameworkCore;
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
         
        public async Task<List<Chat>> GetAllChatFromASender(Guid loginId, Guid userClicked)
        {
            return await _context.Chats
            .Where(x => x.RecieverId == loginId && x.SenderId == userClicked || x.SenderId == loginId && x.RecieverId == userClicked)
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

        public async Task<List<Chat>> GetChatByChatId(Guid loginId, Guid senderId, Guid chatId)
        {
            return await _context.Chats.Where(x => x.SenderId == loginId && x.RecieverId == senderId || x.RecieverId == senderId && x.SenderId == loginId)
            .Include(a=> a.Id).OrderBy(a=> a.DateCreated)
            .ToListAsync();
        }
    }
}
