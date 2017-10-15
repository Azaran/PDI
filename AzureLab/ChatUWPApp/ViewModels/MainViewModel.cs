using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Networking.Connectivity;
using ChatUWPApp.ChatApi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ChatUWPApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IEnumerable<ChatMessage> _chatMessages = new List<ChatMessage>();
        private string _hostname;
        private ChatMessage _newMessage;

        public MainViewModel()
        {
            this.SendNewMessageCommand = new RelayCommand(async () => await this.SendNewMessage());
            this.ClearMessagesCommand = new RelayCommand(async () => await this.ClearMessages());
            this._newMessage = new ChatMessage {Message = "New message...", Sender = this.Hostname};
        }

        public IEnumerable<ChatMessage> ChatMessages
        {
            get => this._chatMessages;
            private set
            {
                if (Equals(value, this._chatMessages)) return;
                this._chatMessages = value;
                this.RaisePropertyChanged();
            }
        }

        private string Hostname => this._hostname ?? (this._hostname = this.ComputerName());

        public ICommand RefreshCommand => new RelayCommand(async () => await this.RefreshChatMessages());

        public ChatMessage NewMessage
        {
            get => this._newMessage;
            set
            {
                this._newMessage = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand SendNewMessageCommand { get; }
        public ICommand ClearMessagesCommand { get; }


        private string ComputerName()
        {
            var hostNames = NetworkInformation.GetHostNames();
            var localName = hostNames.FirstOrDefault(name => name.DisplayName.Contains(".local"));
            var computerName = localName.DisplayName.Replace(".local", "");
            return computerName;
        }

        public async Task RefreshChatMessages()
        {
            var chatApi = new ChatServiceClient();
            var chatMessages = await chatApi.GetAllMessagesAsync();
            this.ChatMessages = new ObservableCollection<ChatMessage>(chatMessages);
        }


        public async Task SendNewMessage()
        {
            var chatApi = new ChatServiceClient();
            await chatApi.SendMessageAsync(this.NewMessage);
            this.NewMessage = new ChatMessage {Sender = this.Hostname};
            await this.RefreshChatMessages();
        }

        private async Task ClearMessages()
        {
            await new ChatServiceClient().ClearMessagesAsync();
            await this.RefreshChatMessages();
        }
    }
}