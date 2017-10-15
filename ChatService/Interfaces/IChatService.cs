using System.Collections.Generic;
using System.ServiceModel;
using ChatService.Models;

namespace ChatService.Interfaces
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        void SendMessage(ChatMessage chatMessage);

        [OperationContract]
        void ClearMessages();

        [OperationContract]
        IEnumerable<ChatMessage> GetAllMessages();
    }
}