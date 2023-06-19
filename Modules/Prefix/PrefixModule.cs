using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using SummaryAttribute = Discord.Commands.SummaryAttribute;
using System.Security.Cryptography;

namespace W_bot.Modules.Prefix
{
    // this Module name, PrefixModule, will be called by AddModule when loading the bot with the available prefix commands
    public class PrefixModule : ModuleBase
    {
        [Command("version")]
        [Alias("ver")]
        public async Task version() => await ReplyAsync("W bot 2.10 .net 7");

        [Command("pm")]
        public async Task test(IUser user, [Remainder] string pm)
        {
            var channel = Context.Channel as SocketTextChannel;
            var items = await channel.GetMessagesAsync(1).FlattenAsync();
            await user.SendMessageAsync(pm);
            await channel.DeleteMessagesAsync(items);
        }

        [Command("ping")]
        public async Task Ping() => await ReplyAsync("Pong! 🏓");

        [Command("isgayokay")]
        public async Task gay() => await ReplyAsync("No ofc it isn't ✝");

        [Command("help")]
        public async Task help()
        {
            await ReplyAsync("#HELP\r\n" +
                // "+isgayokay //See for yourself\r\n" +
                "+ping //Just a test command\r\n" +
                "+8ball [anything] //Tells your fortune\r\n" +
                "+trolleyproblem //Shows video of a trolley flying into a childrens hospital\r\n" +
                "+dream //Shows video of dream being a peadophile\r\n" +
                "+head //Shows video explanation of wich character form Alvin and the chimpmunks gives the best head\r\n" +
                // "+adolfrap //Send W Adolf Hitler rap\r\n" +
                "+smash or pass [anything] //Delivers verdict if someone is for smashing or passing\r\n" +
                "+L (username) //Sends terminator wins gif and pings a user if specified\n" +
                "[Ping the bot] + scan his balls //Scans someones balls\n"+
                "+skill (username) //Sends video of monkeys dancing to skill issuse and mentiones a user if specified\n" +
                "+admin //Show's admin commands\n" +
                "+pfp (username) //Shows your profile picture or someone else's if specified\r\n" +
                "+status [anything] //Changes the bot's status\n" +
                "+version //Displays the bot verison and .net verison\r\n" +
                "\r\n" +
                "Made by: supervidak64");
        }

        [Command("realhelp")]
        public async Task realhelp()
        {
            await ReplyAsync("#HELP\r\n" +
                "+isgayokay //See for yourself\r\n" +
                "+ping //Just a test command\r\n" +
                "+8ball [anything] //Tells your fortune\r\n" +
                "+trolleyproblem //Shows video of a trolley flying into a childrens hospital\r\n" +
                "+dream //Shows video of dream being a peadophile\r\n" +
                "+head //Shows video explanation of wich character form Alvin and the chimpmunks gives the best head\r\n" +
                "+L (username) //Sends terminator wins gif and pings a user if specified\n" +
                "[Ping the bot] + scan his balls //Scans someones balls\n" +
                "+skill (username) //Sends video of monkeys dancing to skill issuse and mentiones a user if specified\n" +
                "+admin //Show's admin commands\n" +
                "+smash or pass [something] //Delivers verdict if someone is for smashing or passing\r\n" +
                "+pfp (username) //Shows your profile picture or someone else's if specified\r\n" +
                "+status [anything] //Changes the bot's status\n" +
                "+version //Displays the bot verison and .net verison\r\n\n" +
                "#Requires NSFW channel\n" +
                "+watermelon //Shows video of a black guy humping a watermelon\r\n" +
                "+nigga or +nigger //Shows edit of guy saying the n word\r\n" +
                "+balls //Sends Markiplier's balls\n" +
                "+correctopinion //Shows you the only correct opinion\r\n" +
                "+ishowshit //Sends video or ishowspeed eating shit\r\n" +
                "+adolfrap //Send W Adolf Hitler rap\r\n" +
                "\r\n" +
                "Made by: supervidak64");
        }

        [RequireNsfw]
        [Command("watermelon")]
        public async Task Melon() => await Context.Channel.SendFileAsync("Media\\watermelonguy.gif");

        [Command("hello")]
        public async Task hello() => await Context.Message.ReplyAsync($"Hello {Context.User.Username}. Nice to meet you!");

        [Command("8ball")]
        //takvo sranje da se ugradi ocu da se ubijem
        private async Task eightball(params string[] args)
        {
            string[] eightballquotes = { "As I see it, yes.", "Ask again later.", "Better not tell you now.", "Cannot predict now.", "Concentrate and ask again.", "Don’t count on it.", "Don’t count on it.", "It is certain.",
                "It is decidedly so.", "Most likely.", "My reply is no.", "My sources say no.", "Outlook not so good.", "Outlook good.", "Reply hazy, try again.", "Signs point to yes", "Very doubtful", "Without a doubt.",
            "Yes.", "Yes - definitely.", "You may rely on it"};
            Random rand = new Random();
            int index = rand.Next(eightballquotes.Length);
            if (args.Length == 0)
            {
                await ReplyAsync("You have to say something in order to recieve a prediction!");
            }
            else
            {
                await ReplyAsync($"{eightballquotes[index]}");
            }
        }

        [Command("spammer20")]
        public async Task spammer()
        {
            for (int i = 0; i < 10; i++)
            {
                await ReplyAsync("test\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\n");
                await ReplyAsync("test\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\ntest\r\n");
            }
        }
        [Command("userinfo")]
        [Alias("user", "whois", "pfp")]
        public async Task UserInfoAsync(SocketUser? user = null)
        {
            if (user == null)
            {
                var embed2 = new EmbedBuilder()
                .WithTitle($"{Context.User.Username}")
                .WithImageUrl(Context.User.GetAvatarUrl(size: 4096) ?? Context.User.GetDefaultAvatarUrl())
                .WithColor(Color.Blue)
                .Build();

                await ReplyAsync(embed: embed2);
            }
            else
            {
                var embed = new EmbedBuilder()
                .WithTitle($"{user.Username}")
                .WithImageUrl(user.GetAvatarUrl(size: 4096) ?? user.GetDefaultAvatarUrl())
                .WithColor(Color.Blue)
                .Build();

                await ReplyAsync(embed: embed);
            }
            /*if (user == null)
            {
                await ReplyAsync(Context.User.GetAvatarUrl());
            }
            else
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"{userInfo.GetAvatarUrl()}");
            }*/
        }

        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo) => ReplyAsync(echo);

        [Command("trolleyproblem")]
        public async Task trolley() => await Context.Channel.SendFileAsync("Media\\trolley.mp4");

        [RequireNsfw]
        [Command("adolfrap")]
        public async Task adolfrap() => await Context.Channel.SendFileAsync("Media\\adolfrap.mp4");

        [Command("dream")]
        public async Task dream() => await Context.Channel.SendFileAsync("Media\\dream.mp4");

        [RequireNsfw]
        [Command("ishowshit")]
        public async Task ishowshit() => await Context.Channel.SendFileAsync("Media\\ishowshit.mp4");

        [RequireNsfw]
        [Command("correctopinion")]
        public async Task correctopinion() => await Context.Channel.SendFileAsync("Media\\correctopinion.mp4");

        [RequireNsfw]
        [Command("nigger")]
        [Alias("nigga")]
        public async Task nigger() => await Context.Channel.SendFileAsync("Media\\nigga.mp4");

        [Command("head")]
        public async Task head() => await Context.Channel.SendFileAsync("Media\\head.mp4");

        [Command("admin")]
        [Discord.Commands.RequireUserPermission(GuildPermission.Administrator)]
        public async Task Admin()
        {
            await ReplyAsync("#Admin\n" +
                "+kick [username] //Kick's a specified user\n" +
                "+ban [username] //Ban's a specified user\n" +
                "+unban [UserId] //Unbans a specified user\n" +
                "+purge [n] //Bulk deletes messages");
        }
    }
}
