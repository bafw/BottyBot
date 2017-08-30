using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
namespace Gavox_Bot
{
    class Bot
    {
        DiscordClient dc;
        CommandService commands;
        public Bot()
        {
            dc = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            dc.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = false;
            });
            commands = dc.GetService<CommandService>();
            commands.CreateCommand("ping").Do(async (e) =>
            {
                await e.Channel.SendMessage("Pong " + e.User.Mention);
            });
            commands.CreateCommand("credits").Do(async (e) =>
            {
                await e.Channel.SendMessage("**Credits : Silphy#2884**");
            });
            commands.CreateCommand("randomuser").Do(async (e) =>
                {
                    Random rnd = new Random();
                    var users = e.Server.Users.ToArray();
                    int randomuser = rnd.Next(users.Length);
                    if(users[randomuser].IsBot)
                    {
                        while (users[randomuser].IsBot)
                        {
                            randomuser = rnd.Next(users.Length);
                        }
                        
                    }
                    await e.Channel.SendMessage("User picked : " + users[randomuser].Name);
                });
            dc.ExecuteAndWait(async () =>
            {
                await dc.Connect("MzUyNDcxNDUzMTQ2Mjg0MDMy.DIh1Cg.TkcAJxXKakYclLzaX42oE-aUhyM", TokenType.Bot);

            });
        }
        
        public DiscordClient Dc { get => dc; set => dc = value; }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
