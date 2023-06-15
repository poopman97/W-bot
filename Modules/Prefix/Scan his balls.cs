using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class Scan_his_balls : ModuleBase<SocketCommandContext>
    {
        [Command("scan his balls")]
        public async Task Balls()
        {
            string[] lenght = {"3cm \nDamn that's small", "6cm \nIf she likes personality", "4mm\nNah bro 💀" };
            Random rand = new Random();
            int index = rand.Next(lenght.Length);
            await ReplyAsync($"{lenght[index]}");
        }
    }
}
