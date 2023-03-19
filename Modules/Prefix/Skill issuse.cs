using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class SkillIssuse : ModuleBase<SocketCommandContext>
    {
        [Command("skill")]
        [Alias("skill issuse")]
        public async Task SkillIssuseCommand(IUser? user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendFileAsync("Media\\Sounds_like_skill_issue.mp4");
            }
            else
            {
                await Context.Channel.SendFileAsync("Media\\Sounds_like_skill_issue.mp4", user.Mention);
            }

        }
    }
}
