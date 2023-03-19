using Discord.Commands;

namespace W_bot.Modules.Prefix
{
    public class Time : ModuleBase<SocketCommandContext>
    {
        //Time is local time in my case utc+1
        [Command("time")]
        public async Task TimeCmd()
        {
            await ReplyAsync("Now is:\n" + DateTime.Now);
        }
    }
}
