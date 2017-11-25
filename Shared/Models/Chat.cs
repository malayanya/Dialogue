using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shared.Models
{
    [DataContract(IsReference = true)]
    public class Chat
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private DateTime _date;
        [DataMember]
        private List<UserChatRelation> _userChatRelations;
        [DataMember]
        private List<Message> _messages = new List<Message>();
        #endregion

        #region Properties
        internal Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        internal List<UserChatRelation> UserChatRelations
        {
            get { return _userChatRelations; }
            private set { _userChatRelations = value; }
        }

        public List<Message> Messages
        {
            get { return _messages; }
        }
        #endregion

        #region Constructor
        public Chat(User user) : this()
        {
            _guid = Guid.NewGuid();
            _date = DateTime.Now;
            new UserChatRelation(user, this);
        }
        private Chat()
        {
            _userChatRelations = new List<UserChatRelation>();
        }
        #endregion

        public override string ToString()
        {
            return Date.ToString(CultureInfo.InvariantCulture);
        }

        #region EntityFrameworkConfiguration
        public class ChatEntityConfiguration : EntityTypeConfiguration<Chat>
        {
            public ChatEntityConfiguration()
            {
                ToTable("Chat");
                HasKey(s => s.Guid);

                Property(p => p.Date)
                    .HasColumnName("Date")
                    .IsRequired();
                
                HasMany(s => s.Messages)
                    .WithRequired(w => w.Chat)
                    .HasForeignKey(w => w.ChatGuid)
                    .WillCascadeOnDelete(true);
                
                HasMany(s => s.UserChatRelations)
                    .WithRequired(w => w.Chat)
                    .HasForeignKey(w => w.ChatGuid)
                    .WillCascadeOnDelete(true);
            }
        }
        #endregion
    }
}
