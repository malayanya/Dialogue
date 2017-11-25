using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
using WalletSimulator.Tools;

namespace WpfApp1
{
    class ChatBot
    {
        private Chat _chat;
        private Random _random = new Random();

        public Chat Chat
        {
            get => _chat;
            set => _chat = value;
        }

        public string Reply()
        {
            if (_chat == null)
            {
                Logger.Log("ChatBot - Chat is not set");
                return null;
            }

            if (_chat.Messages.Count == 0)
            {
                double r = _random.NextDouble();
                if (r < 0.2)
                {
                    return "Hi!";
                } else if (r < 0.4)
                {
                    return "Hello!";
                }
                else if (r < 0.6)
                {
                    return "Greetings!";
                }
                else if (r < 0.8)
                {
                    return "Well, hi.";
                }
                else if (r < 0.9)
                {
                    return "@!$@%&:))!";
                }
                else if (r < 0.95)
                {
                    return "Howdy?";
                }
                else
                {
                    return "...";
                }

            }
            else
            {
                Message lastMessage = _chat.Messages.Last();
                if (lastMessage.User == null)
                {
                    return "Tell me somethin";
                }
                else
                {
                    String last = lastMessage.Text.ToLower();

                    if (last.Contains("hi") || last.Contains("hello"))
                    {
                        return "Hello! How are you?";
                    }

                    if (
                        (last.Contains("how") && last.Contains("are") && last.Contains("you")) || (last.Contains("how") && last.Contains("do") && last.Contains("you")))
                    {
                        return "I'm fine, thank you! And how are you?";
                    }

                    if (last.Contains("robot"))
                    {
                        return "Don't know what you are talking about";
                    }

                    if (last.Contains("?"))
                    {
                        return _random.NextDouble() < 0.5 ? "Yes" : "No";
                    }

                    return "What we'll talk about?";
                }
            }
        }
    }
}
