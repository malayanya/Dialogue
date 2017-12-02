using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Shared;
using Shared.Models;
using WalletSimulator.Tools;

namespace WpfApp1.Views
{
    public partial class ChatListWindow : Window
    {
        private readonly App _app = (App)Application.Current;

        private ChatBot bot;

        private List<Chat> _chats;
        private Chat _currentChat;

        public ChatListWindow()
        {
            InitializeComponent();

            _chats = _app.ChatService.GetAllChats(_app.CurrentUser.Guid);
            lbChatList.ItemsSource = _chats;

            bot = new ChatBot();

            SetCurrentChat(null);
        }

        private void bSendNewMessage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentChat == null)
            {
                Logger.Log("Current chat is null");
                return;
            }

            string text = tbNewMessage.Text;
            if (text.Length == 0)
            {
                Logger.Log("Message text is empty");
                return;
            }

            Message message = new Message(text, _currentChat, _app.CurrentUser);
            _app.ChatService.AddMessage(message);
            AddMessageToList(message);

            tbNewMessage.Text = "";
            tbNewMessage.Focus();

            MakeBotReply();

            Logger.Log("Added message '" + message + "'");
        }

        private void lbChatList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbChatList.SelectedItems.Count == 0)
            {
                SetCurrentChat(null);
            }
            else
            {
                SetCurrentChat((Chat)lbChatList.SelectedItem);
            }
        }

        private void AddMessageToList(Message message)
        {
            Label label = new Label();
            label.Content = message.ToString();
            spMessages.Children.Add(label);

            svMessages.ScrollToBottom();
        }

        private void SetCurrentChat(Chat chat)
        {
            Logger.Log("Set current chat: " + (chat?.ToString() ?? "null"));
            _currentChat = chat;

            if (bot != null) bot.Chat = chat;

            if (chat == null)
            {
                lSelectChatHint.Visibility = Visibility.Visible;
                gChatControls.Visibility = Visibility.Hidden;
                svMessages.Visibility = Visibility.Hidden;

                lbChatList.SelectedIndex = -1;
            }
            else
            {
                lSelectChatHint.Visibility = Visibility.Hidden;
                gChatControls.Visibility = Visibility.Visible;
                svMessages.Visibility = Visibility.Visible;

                spMessages.Children.RemoveRange(0, spMessages.Children.Count);
                foreach (Message message in chat.Messages)
                {
                    AddMessageToList(message);
                }

                lbChatList.SelectedItem = chat;
            }
        }

        private void ReloadChatList()
        {
            Chat oldChat = _currentChat;
            List<Chat> chats = _app.ChatService.GetAllChats(_app.CurrentUser.Guid);
            lbChatList.ItemsSource = chats;

            if (oldChat != null)
            {
                lbChatList.SelectedItem = oldChat;
            }
            else
            {
                lbChatList.SelectedIndex = -1;
            }
        }

        private void MakeBotReply()
        {
            if (bot != null)
            {
                string reply = bot.Reply();
                Message message = new Message(reply, _currentChat, null);
                _app.ChatService.AddMessage(message);
                AddMessageToList(message);
            }
        }

        private void bNewChat_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat(_app.CurrentUser);
            _app.ChatService.AddChat(chat);
            _app.ChatService.AddUserChatRelation(new UserChatRelation(_app.CurrentUser, chat));

            ReloadChatList();
            SetCurrentChat(chat);

            MakeBotReply();
        }

        private void bSignOut_Click(object sender, RoutedEventArgs e)
        {
            _app.SignOut();

            var loginWindow = new SignInWindow();
            loginWindow.Show();
            Close();
        }
    }
}
