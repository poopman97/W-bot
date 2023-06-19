using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class Balls : ModuleBase<SocketCommandContext>
    {
        [RequireNsfw]
        [Command("balls")]
        public async Task BallSend() => await Context.Channel.SendFileAsync("Media\\balls.png");
    }   
}
