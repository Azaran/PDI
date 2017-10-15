using System.Collections.Generic;
using ChatService.Interfaces;
using ChatService.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Persistence
{
    public class ChatMessageRepositorySql : IChatMessageRepository
    {
        private readonly ChatMessageMapper _chatMessageMapper = new ChatMessageMapper();

        public ChatMessageRepositorySql(string connectionString)
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ChatDbContext>();
            contextOptionsBuilder.UseSqlServer(connectionString);
            this.DbContextOptions = contextOptionsBuilder.Options;
        }

        private DbContextOptions<ChatDbContext> DbContextOptions { get; }

        public void AddMessage(ChatMessage chatMessage)
        {
            using (var dbx = new ChatDbContext(this.DbContextOptions))
            {
                dbx.ChatMessages.Add(this._chatMessageMapper.Map(chatMessage));
            }
        }

        public IEnumerable<ChatMessage> GetAllMessages()
        {
            using (var dbx = new ChatDbContext(this.DbContextOptions))
            {
                return this._chatMessageMapper.Map(dbx.ChatMessages);
            }
        }

        public void ClearMessages()
        {
            using (var dbx = new ChatDbContext(this.DbContextOptions))
            {
                dbx.DeleteAll<ChatMessageEntity>();
            }
        }
    }
}