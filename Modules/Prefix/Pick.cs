using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class Pick : ModuleBase<SocketCommandContext>
    {
        [Command("pick")]
        [Alias("choose")] // Aliases that will also trigger the command.
        [Summary("Pick something.")]
        public async Task PickSomething([Remainder] string message = "")
        {
            string[] options = message.Split(new string[] { " or " }, StringSplitOptions.RemoveEmptyEntries);
            string selection = options[new Random().Next(options.Length)];

            // ReplyAsync() is a shortcut for Context.Channel.SendMessageAsync()
            await ReplyAsync($"I choose **{selection}**");
        }
    }
}
