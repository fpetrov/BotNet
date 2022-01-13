using System;
using System.Threading.Tasks;

namespace BotNet.Abstractions
{
    public interface IBot<TOptions, TPayload>
    {
        public TOptions Options { get; set; }
        public IBotCommands<TOptions, TPayload> Commands { get; set; }

        public IBot<TOptions, TPayload> Configure(Action<TOptions> options);
        public IBot<TOptions, TPayload> Configure(TOptions options);

        public Task Start();

        public Task<IBot<TOptions, TPayload>> SendMessage(string text, TPayload payload);
        public Task<IBot<TOptions, TPayload>> SendVideo(string filePath, string text, TPayload payload);
        public Task<IBot<TOptions, TPayload>> SendPhoto(string filePath, string text, TPayload payload);
        public Task<IBot<TOptions, TPayload>> SendDocument(string filePath, TPayload payload);
    }
}
