<p align="center"><img height="188" width="198" src="icon.png"></p>
<h1 align="center">BotNet</h1>

A simple, modular and tiny bot framework for .NET platform.

```csharp
var commands = new BotCommands<BotOptions, BotPayload>()
  .SetCommand("/start", (bot, payload) => bot.SendMessage("Hello, I am a bot!", payload))
  .SetCommand("/token", (bot, payload) => bot.SendMessage($"Current token: {bot.Options.Token}", payload))
  .SetCommand("/document", (bot, payload) => bot.SendDocument("https://website.com/document.word", payload));
            
var bot = new TelegramBot(commands);

await bot
  .Configure(options => {
    options.Token = "Place your token here";
  })
  .Start();
});
```

## Features
- .NET 5 support
- Highly customizible
- Easy to use
- Supports [Vk](https://vk.com/), [Telegram](https://telegram.org/) and [Discord](https://discord.com/) out of the box
> You can easily add bindings to social network you need.

## Documentation
You can find the BotNet documentation at [Wiki](https://fpetrov.github.io/botnet). (Soon)

## License
BotNet is free software distributed under the terms of the MIT license.
