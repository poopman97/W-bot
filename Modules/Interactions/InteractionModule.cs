using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using W_bot.Log;

namespace W_bot.Modules.Interactions
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger _logger;

        public InteractionModule(ConsoleLogger logger)
        {
            _logger = logger;
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("ping", "Receive a pong!")]
        public async Task Ping()
        {
            // New LogMessage created to pass desired info to the console using the existing Discord.Net LogMessage parameters
            await _logger.Log(new LogMessage(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: ping", null));
            // Respond to the user
            await RespondAsync("Pong! 🏓");
        }

        [SlashCommand("8ball", "find your answer!")]
        public async Task EightBall(string question)
        {
            // create a list of possible replies
            var replies = new List<string>();

            // add our possible replies
            replies.Add("yes");
            replies.Add("no");
            replies.Add("maybe");
            replies.Add("hazzzzy....");

            // get the answer
            var answer = replies[new Random().Next(replies.Count - 1)];

            // reply with the answer
            await RespondAsync($"You asked: [**{question}**], and your answer is: [**{answer}**]");
        }

        [SlashCommand("test", "testing")]
        public async Task Test()
        {
            await RespondAsync("Sussy test");
        }

        /*[SlashCommand("Embed", "tests embeded messages")]
        public async Task Embed()
        {
            var embed = new EmbedBuilder
            {
                Title = "Hello",
                Description = "Amongus porn"
            };
            embed.AddField("tf",
                "I like to play [Fortnite](https://fortnite.com) very much.")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer => footer.Text = "Suspicious testing")
                .WithColor(Color.Red)
                .WithTitle("Haha red as in \"Amongus\"")
                .WithDescription("Describing")
                .WithUrl("https://sussy.com")
                .WithCurrentTimestamp();

            await RespondAsync(embed: embed.Build());
        }*/

        [SlashCommand("latency", "Display bot latency")]
        public async Task LatencyAsync()
        {
            int latency = Context.Client.Latency;

            var embed = new EmbedBuilder()
                .WithTitle("Latency")
                .WithDescription($"{latency} ms")
                .WithColor(Color.Teal)
                .WithCurrentTimestamp()
                .Build();

            await RespondAsync(embed: embed);
        }

        [SlashCommand("pfp", "Get a user avatar")]
        public async Task AvatarAsync(
        [Summary("user", "The user to get avatar")] IUser? user = null)
        {
            // If user was not specified or it is null, replace it with interaction executor.
            user ??= Context.User;

            // Build an embed to respond.
            var embed = new EmbedBuilder()
                .WithTitle($"{user.Username}#{user.Discriminator}")
                .WithImageUrl(user.GetAvatarUrl(size: 4096) ?? user.GetDefaultAvatarUrl())
                .WithColor(Color.Blue)
                .Build();

            // Respond to the interaction.
            await RespondAsync(embed: embed);
        }

        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public class AdminModule : InteractionModuleBase<SocketInteractionContext>
        {
            [RequireBotPermission(ChannelPermission.ManageMessages)]
            [SlashCommand("clean", "Delete multiple channel messages")]
            public async Task CleanAsync(
                [Summary("amount", "The number of messages to clean up.")][MinValue(2), MaxValue(500)] int amount)
            {
                if (Context.Channel is not ITextChannel channel)
                    return;

                // Respond to the interaction without text.
                await Context.Interaction.DeferAsync();

                var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                // Due to Discord's limits, it is only possible to delete messages which are less than two weeks old.
                var youngMessages = messages.Skip(1).Where(x => x.Timestamp > DateTime.Now.AddDays(-14));
                await channel.DeleteMessagesAsync(youngMessages);

                var embed = new EmbedBuilder()
                    .WithTitle("Success!")
                    .WithDescription($"{youngMessages.Count()} messages have been successfully cleaned.")
                    .WithColor(Color.Green)
                    .Build();

                // Use FollowupAsync to send a followup message to the interaction.
                await FollowupAsync(embed: embed);
            }
        }

        //[NsfwCommand(true)]
        /*[SlashCommand("ishowshit", "Sends video or ishowspeed eating shit")]
        public async Task IShowShit()
        {
            await Context.Channel.SendFileAsync("Media\\adolfrap.mp4");
        }*/

        [SlashCommand("mute", "Mutes a user for a specified amount of time")]
        public async Task MuteAsync(SocketGuildUser user, [Summary("meserument", "days, hours, minutes")] string meserument, int duration)
        {

            switch (meserument)
            {
                case "minutes":
                    var timespan = TimeSpan.FromMinutes(duration);
                    await user.SetTimeOutAsync(timespan);
                    var embed = new EmbedBuilder()
               .WithDescription($":white_check_mark: {user.Mention} was muted successfully\n**Duration** {duration} minutes")
               .Build();
                    await RespondAsync(embed: embed);
                    break;
                case "hours":
                    var timespan1 = TimeSpan.FromHours(duration);
                    await user.SetTimeOutAsync(timespan1);
                    var embed1 = new EmbedBuilder()
               .WithDescription($":white_check_mark: {user.Mention} was muted successfully\n**Duration** {duration} hours")
               .Build();
                    await RespondAsync(embed: embed1);
                    break;
                case "days":
                    var timespan2 = TimeSpan.FromDays(duration);
                    await user.SetTimeOutAsync(timespan2);
                    var embed2 = new EmbedBuilder()
               .WithDescription($":white_check_mark: {user.Mention} was muted successfully\n**Duration** {duration} days")
               .Build();
                    await RespondAsync(embed: embed2);
                    break;
                default:
                    await RespondAsync("Invalid measurement of time");
                    break;

            }
        }

        [SlashCommand("unmute", "Unmutes a user")]
        [RequireBotPermission(GuildPermission.MuteMembers)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task UnMuteAsync(SocketGuildUser user)
        {
            var timespan = TimeSpan.FromSeconds(1);
            await user.SetTimeOutAsync(timespan);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was unmuted successfully")
                .Build();
            await ReplyAsync(embed: EmbedBuilder);
        }
    }
}
