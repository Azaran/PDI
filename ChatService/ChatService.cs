using System;
using System.Collections.Generic;
using ChatService.Interfaces;
using ChatService.Models;

namespace ChatService
{
    public class ChatService : IChatService
    {
        public ChatService()
        {
            this.ChatMessageRepository = new ChatMessageRepositoryInMemory();
        }

        public ChatService(IChatMessageRepository chatMessageRepository)
        {
            this.ChatMessageRepository = chatMessageRepository;
        }

        private IChatMessageRepository ChatMessageRepository { get; }

        public void SendMessage(ChatMessage chatMessage)
        {
            if (chatMessage == null)
                throw new ArgumentNullException(nameof(chatMessage));
            if (chatMessage.TimeStamp == DateTime.MinValue) chatMessage.TimeStamp = DateTime.Now;

            this.ChatMessageRepository.AddMessage(chatMessage);
        }

        public void ClearMessages()
        {
            this.ChatMessageRepository.ClearMessages();
        }

        public IEnumerable<ChatMessage> GetAllMessages()
        {
            return this.ChatMessageRepository.GetAllMessages();
        }
    }
}