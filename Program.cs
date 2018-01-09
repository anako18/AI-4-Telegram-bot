using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot;
using Telegram.Bot.Types;
using AIMLbot;

namespace Telegram
{
    class Program
    {
        private static readonly Bot.TelegramBotClient bot = new Bot.TelegramBotClient("529358461:AAHaqqnuvnE9e-apOAsKgxT5Qx0m7loEwCg");


        static void Main(string[] args)
        {
            //bot.loadSettings();

            bot.OnMessage += BotOnMessage;
            bot.SetWebhookAsync();

            var me = bot.GetMeAsync().Result;

            Console.Title = me.Username;
            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static async void BotOnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Bot.Types.Message msg = e.Message;

            if (msg == null)
                return;

            if (msg.Type == Bot.Types.Enums.MessageType.TextMessage)
            {
                AIMLbot.Bot AI = new AIMLbot.Bot();

                AI.loadSettings(); // This loads the settings from the config folder

                AI.loadAIMLFromFiles();

                AIMLbot.User me_aiml = new AIMLbot.User("dd", AI);

                Request r = new Request(msg.Text, me_aiml, AI);
                Result res = AI.Chat(r);
                string s = res.ToString();
                await bot.SendTextMessageAsync(msg.Chat.Id, s);
            }

            //if (msg.Type == Bot.Types.Enums.MessageType.TextMessage)
            //{
            //    //await bot.SendTextMessageAsync(msg.Chat.Id, "Здарова " + msg.From.FirstName 
            //}

        }
    }
}
