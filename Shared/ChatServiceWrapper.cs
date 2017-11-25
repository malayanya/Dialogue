using Shared.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Shared
{
    public class ChatServiceWrapper
    {
        private readonly ChannelFactory<IChatService> _channelFactory;
        private readonly IChatService _client;

        public ChatServiceWrapper()
        {
            _channelFactory = new ChannelFactory<IChatService>("Server");
            _client = _channelFactory.CreateChannel();
        }

        ~ChatServiceWrapper()
        {
            _channelFactory.Close();
        }

        public void AddMessage(Message message)
        {
            _client.AddMessage(message);
        }

        public bool UserExists(string login)
        {
            return _client.UserExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return _client.GetUserByLogin(login);
        }

        public void AddUser(User user)
        {
            _client.AddUser(user);
        }

        public void AddChat(Chat chat)
        {
            _client.AddChat(chat);
        }

        public List<Chat> GetAllChats(Guid userGuid)
        {
            return _client.GetAllChats(userGuid);
        }

        public void AddUserChatRelation(UserChatRelation userChat)
        {
            _client.AddUserChatRelation(userChat);
        }

        public void DeleteUserChatRelation(UserChatRelation userChat)
        {
            _client.DeleteUserChatRelation(userChat);
        }
    }
}
