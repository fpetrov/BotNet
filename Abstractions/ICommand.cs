using System;

namespace BotNet.Abstractions
{
    public interface ICommand<TOptions, TPayload>
    {
        public string Name { get; set; }
        public Action<IBot<TOptions, TPayload>, TPayload> Action { get; set; }
        
        public void Execute(IBot<TOptions, TPayload> bot, TPayload payload) 
            => Action.Invoke(bot, payload);
    }
}
