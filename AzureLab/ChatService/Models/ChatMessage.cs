using System;
using System.Runtime.Serialization;

namespace ChatService.Models
{
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Sender { get; set; }
    }
}