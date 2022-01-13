using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BotNet.Abstractions;
using BotNet.Models;

namespace BotNet.Core
{
    public class BotCommands<TOptions, TPayload> : IBotCommands<TOptions, TPayload>
    {
        private Dictionary<string, ICommand<TOptions, TPayload>> Commands { get; }
        public ICommand<TOptions, TPayload> UnknownCommand { get; set; }
        public IModuleProvider<TOptions, TPayload> ModuleProvider { get; set; }
        private List<char> Prefixes { get; set; }

        public BotCommands()
        {
            Commands = new Dictionary<string, ICommand<TOptions, TPayload>>();
            Prefixes = new List<char>();
            ModuleProvider = new ModuleProvider<TOptions, TPayload>();

            var modules = ModuleProvider
                .GetModules(Assembly.GetEntryAssembly());

            ImportCommands(modules);
        }

        public BotCommands(params Assembly[] moduleAssemblies) : this()
        {
            foreach (var moduleAssembly in moduleAssemblies)
            {
                var modules = ModuleProvider
                    .GetModules(moduleAssembly);
                
                ImportCommands(modules);
            }
        }

        public IBotCommands<TOptions, TPayload> ExecuteCommand(string name, IBot<TOptions, TPayload> bot, TPayload payload)
        {
            name = name.ToLower();

            if (Prefixes.Count > 0)
            {
                var prefix = name[0];
                name = name.TrimStart(prefix);

                if (Commands.ContainsKey(name))
                {
                    if (Prefixes.Contains(prefix))
                        Commands[name].Execute(bot, payload);
                }
                else
                    UnknownCommand?.Execute(bot, payload);

                return this;
            }

            if (Commands.ContainsKey(name))
                Commands[name].Execute(bot, payload);
            else
                UnknownCommand?.Execute(bot, payload);

            return this;
        }

        public IBotCommands<TOptions, TPayload> Map(Predicate<UpdateRequest> statement)
        {
            var updateRequest = new UpdateRequest();
            
            if (statement.Invoke(updateRequest))
            {
                Console.WriteLine("Yep, it's true!");
            }
            
            return this;
        }

        public IBotCommands<TOptions, TPayload> SetCommand(string name, Action<IBot<TOptions, TPayload>, TPayload> command)
        {
            name = name.ToLower();

            if (!Commands.ContainsKey(name))
                Commands.Add(name, new CommandBase<TOptions, TPayload>(name, command));

            return this;
        }

        public IBotCommands<TOptions, TPayload> SetCommandModule<TModule>(params object[] dependencies)
        {
            var commands = ModuleProvider.GetCommands(typeof(TModule), dependencies);

            ImportCommands(commands);

            return this;
        }

        public IBotCommands<TOptions, TPayload> SetPrefixes(params char[] prefixes)
        {
            Prefixes = prefixes.ToList();

            return this;
        }

        public IBotCommands<TOptions, TPayload> SetUnknownCommand(Action<IBot<TOptions, TPayload>, TPayload> command)
        {
            UnknownCommand = new CommandBase<TOptions, TPayload>(string.Empty, command);

            return this;
        }

        // Additional functions.
        private void ImportCommands(IEnumerable<ICommand<TOptions, TPayload>> commands)
        {
            foreach (var command in commands)
                SetCommand(command.Name, command.Action);
        }
    }
}
