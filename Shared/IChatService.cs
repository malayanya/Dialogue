using System;
using System.Collections.Generic;
using System.ServiceModel;
using Shared.Models;

namespace Shared
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        bool UserExists(string login);

        [OperationContract]
        User GetUserByLogin(string login);

        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        void AddChat(Chat chat);
        
        [OperationContract]
        void AddMessage(Message message);

        [OperationContract]
        List<Chat> GetAllChats(Guid userGuid);

        [OperationContract]
        void AddUserChatRelation(UserChatRelation userChat);

        [OperationContract]
        void DeleteUserChatRelation(UserChatRelation userChat);

    }
}
