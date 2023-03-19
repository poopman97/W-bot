using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class SamshOrPass : ModuleBase<SocketCommandContext>
    {
        [Command("smash or pass")]
        private async Task sex(params string[] args)
        {
            string[] smash = { "I'd definitely smash that!",
              "I don't know, it's a tough call... I'll pass.",
              "Definitely a pass for me.",
              "100% smash material right there.",
              "I'm not sure, let me think about it... Smash!",
              "Pass, for sure.",
              "I'd smash, but only if nobody ever finds out.",
              "Pass, but only because I'm already taken.",
              "Sorry, I don't swing that way... Pass.",
              "Definitely smash-worthy material!",};
            Random rand = new Random();
            int index = rand.Next(smash.Length);
            if (args.Length == 0)
            {
                await ReplyAsync("You have to say something in order to recieve an anwser!");
            }
            else
            {
                await ReplyAsync($"{smash[index]}");
            }
        }
    }
}
