using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace Shared.Models
{
    [DataContract(IsReference = true)]
    public class UserChatRelation
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private Guid _userGuid;
        [DataMember]
        private Guid _chatGuid;
        [DataMember]
        private Chat _chat;
        [DataMember]
        private User _user;

        #endregion

        #region Properties
        private Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        internal Guid UserGuid
        {
            get { return _userGuid; }
            private set { _userGuid = value; }
        }
        internal Guid ChatGuid
        {
            get { return _chatGuid; }
            private set { _chatGuid = value; }
        }

        public Chat Chat
        {
            get { return _chat; }
            private set { _chat = value; }
        }
        internal User User
        {
            get { return _user; }
            private set { _user = value; }
        }
        #endregion

        #region Constructor
        public UserChatRelation(User user, Chat chat)
        {
            _guid = Guid.NewGuid();
            _userGuid = user.Guid;
            _chatGuid = chat.Guid;
            _chat = chat;
            _user = user;
            user.UserChatRelations.Add(this);
            chat.UserChatRelations.Add(this);
        }
        private UserChatRelation()
        {
        }
        #endregion

        #region EntityConfiguration
        public class UserChatRelationEntityConfiguration : EntityTypeConfiguration<UserChatRelation>
        {
            public UserChatRelationEntityConfiguration()
            {
                ToTable("UserChatRelation");
                HasKey(s => s.Guid);

                Property(p => p.UserGuid)
                    .HasColumnName("UserGuid")
                    .IsRequired();
                Property(p => p.ChatGuid)
                    .HasColumnName("ChatGuid")
                    .IsRequired();
            }
        }
        #endregion
    }
}
