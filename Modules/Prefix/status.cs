using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class Status : ModuleBase<SocketCommandContext>
    {
        [Command("status1")]
        public async Task status()
        {
            await Context.Client.SetGameAsync("Fortnite");
            await ReplyAsync("Done");

            await Task.CompletedTask;
        }

        [Command("status2")]
        public async Task TestAsync()
        {
            await Context.Client.SetGameAsync("eating doritos");
            await ReplyAsync("Done");

            await Task.CompletedTask;
        }

        [Command("status")]
        public async Task StatusContext([Remainder] string status)
        {
            await Context.Client.SetGameAsync(status);
            await ReplyAsync("Set the status to " + status);


            await Task.CompletedTask;
        }
    }
}
