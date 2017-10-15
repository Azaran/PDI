using System.Collections.Generic;
using ChatService.Models;

namespace ChatService.Interfaces
{
    public interface IChatMessageRepository
    {
        void AddMessage(ChatMessage chatMessage);
        IEnumerable<ChatMessage> GetAllMessages();
        void ClearMessages();
    }
}