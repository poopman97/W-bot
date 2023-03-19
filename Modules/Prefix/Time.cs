using Discord.Commands;

namespace W_bot.Modules.Prefix
{
    public class Time : ModuleBase<SocketCommandContext>
    {
        [Command("time")]
        public async Task TimeCmd()
        {
            await ReplyAsync("Now is:\n" + DateTime.Now);
        }
    }
}
