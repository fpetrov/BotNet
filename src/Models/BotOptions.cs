using System;
using System.Collections.Generic;

namespace BotNet.Models
{
    public partial class BotOptions
    {
        public string Token { get; set; }
        public ulong GroupId { get; set; }
    }
}