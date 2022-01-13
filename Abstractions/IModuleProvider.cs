using System;
using System.Collections.Generic;
using System.Reflection;

namespace BotNet.Abstractions
{
    public interface IModuleProvider<TOptions, TPayload>
    {
        public IEnumerable<ICommand<TOptions, TPayload>> GetModules(Assembly assembly);
        public IEnumerable<ICommand<TOptions, TPayload>> GetCommands(Type commandModule, params object[] dependencies);
    }
}
