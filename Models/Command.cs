using BotNet.Abstractions;
using System;

namespace BotNet.Models
{
    public class CommandBase<TOptions, TPayload> : ICommand<TOptions, TPayload>
    {
        public string Name { get; set; }

        public Action<IBot<TOptions, TPayload>, TPayload> Action { get; set; }

        public CommandBase(string name, Action<IBot<TOptions, TPayload>, TPayload> action)
        {
            Name = name;
            Action = action;
        }
    }
}
