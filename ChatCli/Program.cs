using System;
using System.ServiceModel;
using ChatService.Interfaces;
using ChatService.Models;

namespace ChatCli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var endpoint =
                new EndpointAddress(
                    new Uri(string.Format("http://localhost:26007/ChatApi.svc", Environment.MachineName)));
            var channelFactory = new ChannelFactory<IChatService>(binding, endpoint);
            var serviceClient = channelFactory.CreateChannel();

            serviceClient.SendMessage(new ChatMessage {Message = "Message", Sender = "Sender"});

            var messages = serviceClient.GetAllMessages();

            foreach (var chatMessage in messages)
                Console.WriteLine($"{chatMessage.TimeStamp}, {chatMessage.Sender}> {chatMessage.Message}");
            Console.ReadKey();
        }
    }
}