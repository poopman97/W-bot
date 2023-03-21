using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace W_bot.Modules.Prefix
{
    public class Time : ModuleBase
    {
        //Time is local time in my case utc+1
        [Command("time")]
        public async Task TimeCmd()
        {
            await ReplyAsync("Now is:\n" + DateTime.Now);
        }

        //[Command("myadmin")]
        //public async Task<RestRole> CreateRoleAsync(string name, GuildPermissions? permissions = null)
        //{
        //    await ReplyAsync("test");
        //
    }
}
