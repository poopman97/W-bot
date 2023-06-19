using Discord;
using Discord.Commands;
using Discord.WebSocket;
using W_bot.Log;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot
{
    public class PrefixHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;


        // Retrieve client and CommandService instance via ctor
        public PrefixHandler(DiscordSocketClient client, CommandService commands, IConfigurationRoot config)
        {
            _commands = commands;
            _client = client;
            _config = config;
        }

        public Task InitializeAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            return Task.CompletedTask;
        }

        public void AddModule<T>()
        {
            _commands.AddModuleAsync<T>(null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            SocketGuildUser? socketGuildUser = message.Author as SocketGuildUser;
            //manage_message = socketGuildUser.GuildPermissions.ViewAuditLog;
            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasStringPrefix("+", ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);

            var text = message.ToString().ToLower();

            switch (text)
            {
                case "hi bot":
                    await message.Channel.SendMessageAsync("Hello " + message.Author.Username + "!");
                    break;
                case "hello bot":
                    await message.Channel.SendMessageAsync("Hello " + message.Author.Username + "!");
                    break;
            }

            if (text.Contains("nigger") && text != "+nigga" && text != "+nigger")
            {
                await message.DeleteAsync();
                await message.Channel.SendMessageAsync("You can't say that!\n\nYou're gonna get cancelled on twitter! \nMessage deleted.");
            }

            if (text.Contains("nigga") && text != "+nigga" && text != "+nigger")
            {
                await message.DeleteAsync();
                await message.Channel.SendMessageAsync("You can't say that!\n\nYou're gonna get cancelled on twitter! \nMessage deleted."); ;
            }
        }
    }
}
