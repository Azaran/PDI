using System;

namespace ChatService.Persistence
{
    public class ChatMessageEntity
    {
        public DateTime TimeStamp { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}