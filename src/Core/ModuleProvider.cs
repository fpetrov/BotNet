using BotNet.Abstractions;
using BotNet.Attributes;
using BotNet.Extensions;
using BotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BotNet.Core
{
    public class ModuleProvider<TOptions, TPayload> : IModuleProvider<TOptions, TPayload>
    {
        public IEnumerable<ICommand<TOptions, TPayload>> GetCommands(Type commandModule, params object[] dependencies)
        {
            var methods = commandModule
                    .GetTypeInfo().DeclaredMethods
                    .Where(method => method.GetCustomAttribute<CommandAttribute>() != null);

            var moduleClass = Activator.CreateInstance(commandModule, dependencies);

            foreach (var method in methods)
            {
                var commandName = method.GetCustomAttribute<CommandAttribute>().Name ?? method.Name;

                yield return new CommandBase<TOptions, TPayload>(
                    commandName,
                    (bot, payload) => method.Invoke(moduleClass, new object[] { bot, payload }));
            }
        }

        public IEnumerable<ICommand<TOptions, TPayload>> GetModules(Assembly assembly)
        {
            var commandModules = assembly
                .GetTypesWithAttribute<CommandModuleAttribute>();

            foreach (var commandModule in commandModules)
            {
                if (!commandModule.HasDefaultConstructor()) 
                    continue;
                
                var commands = GetCommands(commandModule);

                foreach (var command in commands)
                    yield return command;
            }
        }
    }
}
