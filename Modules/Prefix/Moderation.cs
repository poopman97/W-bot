using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W_bot.Modules.Prefix
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        //old code
        /*[Command("kick")]
        [Summary("Kick a user from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await targetUser.KickAsync(reason);
            await ReplyAsync($"**{targetUser}** has been kicked. L + bozo");
        }*/

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers, ErrorMessage = "You don't have the permission ``kick_member``!")]
        public async Task KickMember(SocketGuildUser user = null, [Remainder] string reason = null)
        {

            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await user.KickAsync();

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was kicked\n**Reason** {reason}")
                .Build();
            await ReplyAsync(embed: EmbedBuilder);
        }

        //old code
        /*[Command("ban")]
        [Summary("Ban a user from the server")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            await Context.Guild.AddBanAsync(targetUser.Id, 0, reason);
            await ReplyAsync($"**{targetUser}** has been banned. L + bozo");
        }*/

        [Command("myban0")]
        [Summary("Ban a user from the server")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Ban0(SocketGuildUser targetUser, [Remainder] string reason = "No reason provided.")
        {
            var channel = Context.Channel as SocketTextChannel;
            await Context.Guild.AddBanAsync(targetUser.Id, 0, reason); ;
            //await ReplyAsync($"**{targetUser}** has been banned. L + bozo");
            await channel.DeleteMessageAsync(3);
        }

        //old code
        /*[Command("unban")]
        [Summary("Unban a user from the server")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Unban(ulong targetUser)
        {
            await Context.Guild.RemoveBanAsync(targetUser);
            await Context.Channel.SendMessageAsync($"The user has been unbanned. Bro was lucky");
        }*/

        [Command("purge")]
        [Summary("Bulk deletes messages in chat")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Purge(int delNumber)
        {
            var channel = Context.Channel as SocketTextChannel;
            var items = await channel.GetMessagesAsync(delNumber + 1).FlattenAsync();
            await channel.DeleteMessagesAsync(items);
            var embed = new EmbedBuilder()
                    .WithDescription(":white_check_mark: " + $"{delNumber} messages have been successfully cleaned.")
                    .WithColor(Color.Green)
                    .Build();

            await ReplyAsync(embed: embed);
        }

        [Command("ban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "You don't have the permission ``ban_member``!")]
        public async Task BanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was banned\n**Reason** {reason}")
                .Build();
            await ReplyAsync(embed: EmbedBuilder);
        }

        [Command("unban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "You don't have the permission ``ban_member``!")]
        public async Task UnBanMember(ulong user)
        {

            await Context.Guild.RemoveBanAsync(user);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: The user was unbanned successfully")
                .Build();
            await ReplyAsync(embed: EmbedBuilder);
        }

        [Command("mute")]
        [RequireBotPermission(GuildPermission.MuteMembers)]
        [RequireUserPermission(GuildPermission.MuteMembers, ErrorMessage = "You don't have the permission ``ban_member``!")]
        public async Task MuteMember(SocketGuildUser user, [Remainder] int duration)
        {
            var timespan = TimeSpan.FromMinutes(duration);
            await user.SetTimeOutAsync(timespan);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} was muted successfully\n**Duration** {duration} minutes")
                .Build();
            await ReplyAsync(embed: EmbedBuilder);
        }
    }
}
