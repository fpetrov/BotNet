using System;

namespace BotNet.Models
{
    public class UpdateRequest
    {
        public string Sender { get; set; }
        public Message Message { get; set; }
        public DateTime Created { get; set; }
    }

    public class Message
    {
        public string Text { get; set; }
    }
}