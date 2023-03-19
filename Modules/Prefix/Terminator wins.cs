using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class TerminatorWins : ModuleBase<SocketCommandContext>
    {
        [Command("L")]
        [Alias("W")]
        public async Task Terminator_Wins(SocketUser? user = null)
        {
            if (user == null)
            {
                await ReplyAsync("https://tenor.com/view/terminator-wins-gif-25847916");
            }
            else
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"{user.Mention} \n https://tenor.com/view/terminator-wins-gif-25847916");
            }

        }

        [Command("xinc")]
        public async Task xinc()
        {
            await ReplyAsync("https://tenor.com/view/aroojtwt-gif-25910279");
        }
    }
}
