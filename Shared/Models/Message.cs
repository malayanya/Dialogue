using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace Shared.Models
{
    [DataContract]
    public class Message
    {
        #region Fields

        [DataMember] private Guid _guid;
        [DataMember] private string _text;
        [DataMember] private DateTime _date;

        [DataMember] private Guid _chatGuid;
        [DataMember] private Guid _userGuid;

        [DataMember] private Chat _chat;
        [DataMember] private User _user;

        #endregion

        #region Properties

        private Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        private DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        internal Guid ChatGuid
        {
            get { return _chatGuid; }
            private set
            {
                _chatGuid = value;
                if (_chat != null && _chat.Guid != _chatGuid)
                {
                    _chat = null;
                }
            }
        }

        internal Guid UserGuid
        {
            get { return _userGuid; }
            private set
            {
                _userGuid = value;
                if (_user != null && _user.Guid != _userGuid)
                {
                    _user = null;
                }
            }
        }

        public Chat Chat
        {
            get { return _chat; }
            private set
            {
                _chat = value;
                if (_chat.Guid != _chatGuid)
                    _chatGuid = _chat.Guid;
            }
        }

        public User User
        {
            get { return _user; }
            private set
            {
                _user = value;
                if (_user != null && _user.Guid != _userGuid)
                    _userGuid = _user.Guid;
            }
        }

        #endregion

        #region Constructor

        public Message(string text, Chat chat, User user)
        {
            _guid = Guid.NewGuid();
            _text = text;
            _date = DateTime.Now;

            Chat = chat;
            User = user;
            Chat.Messages.Add(this);
        }

        public Message()
        {

        }

        #endregion

        public override string ToString()
        {
            return _date.ToShortTimeString() + " " + (User == null ? "Bot" : User.Login) + ": " + _text;
        }

        public void DeleteDatabaseValues()
        {
            _chat = null;
            _user = null;
        }

        #region EntityFrameworkConfiguration

        public class MessageEntityConfiguration : EntityTypeConfiguration<Message>
        {
            public MessageEntityConfiguration()
            {
                ToTable("Message");
                HasKey(s => s.Guid);

                Property(p => p.Text)
                    .HasColumnName("Text")
                    .IsRequired();
                Property(p => p.Date)
                    .HasColumnName("Date")
                    .IsRequired();
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
