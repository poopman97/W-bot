/*This file is part of the W bot distribution (https://github.com/poopman97/W-bot).
Copyright(c) 2023 Vidak Nedeljković.

This program is free software: you can redistribute it and/or modify  
it under the terms of the GNU Affero General Public License as published by  
the Free Software Foundation, version 3.
 
This program is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of 
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
Affero General Public License for more details.
 
You should have received a copy of the GNU Affero General Public License 
along with this program. If not, see <http://www.gnu.org/licenses/>.*/


using Discord.Interactions;
using Discord.WebSocket;
using W_bot.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Yaml;
using Discord.Commands;
using Discord;
using W_bot.Modules.Prefix;
using W_bot.Modules;

namespace W_bot
{
    public class program
    {
        private DiscordSocketClient _client;        
        
        // Program entry point
        public static Task Main(string[] args) => new program().MainAsync();


        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
            // this will be used more later on
            .SetBasePath(AppContext.BaseDirectory)
            // I chose using YML files for my config data as I am familiar with them
            .AddYamlFile("config.yml")
            .Build();
            
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services
            // Add the configuration to the registered services
            .AddSingleton(config)
            // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All,
                //GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.MessageContent,
                LogGatewayIntentWarnings = false,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                LogLevel = LogSeverity.Debug
            }))
			// Adding console logging
            .AddTransient<ConsoleLogger>()
            // Used for slash commands and their registration with Discord
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            // Required to subscribe to the various client events used in conjunction with Interactions
            .AddSingleton<InteractionHandler>()
            // Adding the prefix Command Service
            .AddSingleton(x => new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                DefaultRunMode = Discord.Commands.RunMode.Async
            }))
            // Adding the prefix command handler
            .AddSingleton<PrefixHandler>())
            .Build();

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = provider.GetRequiredService<PrefixHandler>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.Embed>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.PrefixModule>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.Moderation>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.TerminatorWins>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.Time>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.SamshOrPass>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.Status>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.Pick>();
            prefixCommands.AddModule<W_bot.Modules.Prefix.SkillIssuse>();
            await prefixCommands.InitializeAsync();


            // Subscribe to client log events
            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            // Subscribe to slash command log events
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () =>
            {
                // If running the bot with DEBUG flag, register all commands to guild specified in config
                if (IsDebug())
                    // Id of the test guild can be provided from the Configuration object
                    await commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testGuild"]), true);
                else
                    // If not debug, register commands globally
                    await commands.RegisterCommandsGloballyAsync(true);
            };


            await _client.LoginAsync(Discord.TokenType.Bot, config["tokens:discord"]);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}