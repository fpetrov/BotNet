using System;
using BotNet.Models;

namespace BotNet.Abstractions
{
    public interface IBotCommands<TOptions, TPayload>
    {
        public ICommand<TOptions, TPayload> UnknownCommand { get; set; }
        public IModuleProvider<TOptions, TPayload> ModuleProvider { get; set; }
        public IBotCommands<TOptions, TPayload> Map(Predicate<UpdateRequest> statement);
        public IBotCommands<TOptions, TPayload> SetCommand(string name, Action<IBot<TOptions, TPayload>, TPayload> command);
        public IBotCommands<TOptions, TPayload> SetCommandModule<TCommandModule>(params object[] dependencies);
        public IBotCommands<TOptions, TPayload> SetUnknownCommand(Action<IBot<TOptions, TPayload>, TPayload> command);
        public IBotCommands<TOptions, TPayload> SetPrefixes(params char[] prefixes);
        public IBotCommands<TOptions, TPayload> ExecuteCommand(string name, IBot<TOptions, TPayload> bot, TPayload payload);
    }
}
