using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

class ProgramDiscord
{
    private DiscordSocketClient _client;

    static void Main(string[] args) => new ProgramDiscord().RunBotAsync().GetAwaiter().GetResult();

    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();

        // Token bota
        string botToken = "YOUR_DISCORD_BOT_TOKEN";

        _client.Log += Log;

        await _client.LoginAsync(TokenType.Bot, botToken);
        await _client.StartAsync();

        _client.MessageReceived += HandleMessageReceived;

        await Task.Delay(-1);
    }

    private Task Log(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

    private async Task HandleMessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        if (message.Content.StartsWith("!hello"))
        {
            await message.Channel.SendMessageAsync($"Witaj, {message.Author.Username}! Jak mogę Ci pomóc?");
        }
    }
}
