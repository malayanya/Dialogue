using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared
{
    public class ChatServiceEmulator : IChatService
    {
        private readonly List<Chat> _chats = new List<Chat>();
        private readonly List<Message> _messages = new List<Message>();
        private readonly List<User> _users = new List<User>();
        private readonly List<UserChatRelation> _userChatRelations = new List<UserChatRelation>();

        public ChatServiceEmulator()
        {
            User user = new User("Mike", "Jhonson", "mikejhonson@gmail.com", "asd", "dsa");
            Chat chat = new Chat(user);

            AddUser(user);
            AddChat(chat);
            AddUserChatRelation(new UserChatRelation(user, chat));
            AddMessage(new Message("Hi, I'm a bot. What are you going to talk about?", chat, null));
        }

        public void AddChat(Chat chat)
        {
            if (!_chats.Contains(chat)) _chats.Add(chat);
        }

        public void AddMessage(Message message)
        {
            if (!_messages.Contains(message)) _messages.Add(message);
        }

        public void AddUser(User user)
        {
            if (!_users.Contains(user)) _users.Add(user);
        }

        public void AddUserChatRelation(UserChatRelation userChat)
        {
            foreach (UserChatRelation relation in _userChatRelations)
            {
                if (relation.Chat == userChat.Chat && relation.User == userChat.User)
                {
                    return;
                }
            }

            _userChatRelations.Add(userChat);
        }

        public void DeleteUserChatRelation(UserChatRelation userChat)
        {
            _userChatRelations.Remove(userChat);
        }

        public List<Chat> GetAllChats(Guid userGuid)
        {
            List<Chat> chats = new List<Chat>();
            foreach (UserChatRelation relation in _userChatRelations)
            {
                if (relation.UserGuid == userGuid)
                {
                    chats.Add(relation.Chat);
                }
            }

            return chats;
        }

        public User GetUserByLogin(string login)
        {
            foreach (User user in _users)
            {
                if (user.Login.Equals(login)) 
                {
                    return user;
                }
            }

            return null;
        }

        public bool UserExists(string login)
        {
            return GetUserByLogin(login) != null;
        }
    }
}
